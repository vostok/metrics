using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Model;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Timer
{
    /// <inheritdoc cref="ITimer"/>
    /// <summary>
    /// <para>
    /// Histogram allows you to estimate the distribution of values.
    /// </para>
    /// <para>
    /// You configure Histogram by specifying buckets.
    /// Each reported value finds the corresponding bucket and increments the value inside.
    /// Then the buckets are periodically scraped.
    /// </para>
    /// <para>
    /// Histograms are aggregated server-side.
    /// Output of aggregation includes total count, sum and percentiles.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Consider using <see cref="Timer"/> instead of Histogram.
    /// Timer provides exact percentiles and doesn't require any configuration.
    /// However, it is not suitable for high workloads.
    /// </para>
    /// <para>
    /// To create a Histogram use <see cref="HistogramFactoryExtensions">extensions</see> for <see cref="IMetricContext"/>.
    /// </para>
    /// <para>
    /// Build custom <see cref="HistogramBuckets"/> to customize histogram resolution. Avoid creating lots of buckets (> 100).
    /// </para>
    /// <para>
    /// Call <see cref="IDisposable.Dispose"/> to stop scraping the metric.
    /// </para>
    /// </remarks>
    internal class Histogram : ITimer, IScrapableMetric
    {
        private readonly MetricTags tags;
        private readonly HistogramConfig config;
        private readonly IDisposable registration;

        private readonly long[] bucketCounters;
        private readonly MetricTags[] bucketTags;

        public Histogram([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] HistogramConfig config)
        {
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);

            bucketCounters = new long[config.Buckets.Count];
            bucketTags = new MetricTags[config.Buckets.Count];

            // TODO(iloktionov): use name tag instead?
            for (var i = 0; i < config.Buckets.Count - 1; i++)
                bucketTags[i] = tags.Append(WellKnownTagKeys.UpperBound, config.Buckets[i].RightBound.ToString(CultureInfo.InvariantCulture));

            bucketTags[config.Buckets.Count - 1] = tags.Append(WellKnownTagKeys.UpperBound, "+Inf");
        }

        public string Unit => config.Unit;

        public void Report(double value)
        {
            if (double.IsNaN(value))
                return;

            Interlocked.Increment(ref bucketCounters[FindBucketIndex(value)]);
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            for (var i = 0; i < bucketCounters.Length; i++)
            {
                yield return new MetricEvent(Interlocked.Exchange(ref bucketCounters[i], 0L), bucketTags[i], 
                    timestamp, config.Unit, WellKnownAggregationTypes.Histogram, config.AggregationParameters);
            }
        }

        public void Dispose()
            => registration.Dispose();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FindBucketIndex(double value)
        {
            var buckets = config.Buckets;

            if (buckets[0].ContainsValue(value) || double.IsNegativeInfinity(value))
                return 0;

            if (buckets[buckets.Count - 1].ContainsValue(value))
                return buckets.Count - 1;

            var leftIndex = 1;
            var rightIndex = buckets.Count - 2;

            while (rightIndex >= leftIndex)
            {
                var index = leftIndex + (rightIndex - leftIndex) / 2;
                var comparison = buckets[index].CompareWithValue(value);
                if (comparison == 0)
                    return index;

                if (comparison < 0)
                    rightIndex = index - 1;
                else
                    leftIndex = index + 1;
            }

            return -1;
        }
    }
}
