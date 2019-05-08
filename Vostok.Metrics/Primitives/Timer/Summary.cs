using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Commons.Threading;
using Vostok.Metrics.Model;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Timer
{
    /// <inheritdoc cref="ITimer"/>
    /// <summary>
    /// <para>
    /// Summary allows you to estimate the distribution of values locally, without any server-side aggregation.
    /// </para>
    /// <para>
    /// You configure Summary's by specifying desired quantiles and allowed memory usage.
    /// An online algorithm then computes quantiles on the stream of reported values.
    /// Internal state is periodically scraped to obtain metrics.
    /// </para>
    /// <para>
    /// Summaries are aggregated client-side.
    /// Output of aggregation includes total count, avg and approximate percentiles.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>Summary only computes approximate quantiles and is limited to a single process.</para>
    /// <para>Consider using <see cref="Timer"/> or <see cref="Histogram"/> to aggregate values from multiple processes.</para>
    /// <para>Consider using <see cref="Timer"/> to obtain exact values of quantiles.</para>
    /// <para>
    /// To create a Summary, use <see cref="SummaryFactoryExtensions">extensions</see> for <see cref="IMetricContext"/>.
    /// </para>
    /// <para>
    /// Call <see cref="IDisposable.Dispose"/> to stop scraping the metric.
    /// </para>
    /// </remarks>
    internal class Summary : ITimer, IScrapableMetric
    {
        private readonly SummaryConfig config;
        private readonly IDisposable registration;

        private readonly MetricTags countTags;
        private readonly MetricTags avgTags;
        private readonly MetricTags[] quantileTags;

        private readonly double[] sample;
        private readonly object snapshotSync;
        private double[] snapshot;
        private int count;

        public Summary([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] SummaryConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            countTags = tags.Append(WellKnownTagKeys.Aggregate, WellKnownTagValues.AggregateCount);
            avgTags = tags.Append(WellKnownTagKeys.Aggregate, WellKnownTagValues.AggregateAverage);

            if (config.Quantiles != null)
                quantileTags = PrepareQuantileTags(config, tags);

            registration = context.Register(this, config.ScrapePeriod);
            sample = new double[config.BufferSize];
            snapshotSync = new object();
        }

        public string Unit => config.Unit;

        public void Report(double value)
        {
            var newCount = Interlocked.Increment(ref count);
            if (newCount <= sample.Length)
            {
                Interlocked.Exchange(ref sample[newCount - 1], value);
            }
            else
            {
                var randomIndex = ThreadSafeRandom.Next(newCount);
                if (randomIndex < sample.Length)
                    Interlocked.Exchange(ref sample[randomIndex], value);
            }
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            lock (snapshotSync)
            {
                var countBeforeReset = Interlocked.Exchange(ref count, 0);

                var snapshotSize = Math.Min(countBeforeReset, sample.Length);

                if (snapshot == null || snapshot.Length < snapshotSize)
                    snapshot = new double[snapshotSize];

                for (var i = 0; i < snapshotSize; i++)
                    snapshot[i] = Interlocked.CompareExchange(ref sample[i], 0d, 0d);

                Array.Sort(snapshot);

                var result = new List<MetricEvent>
                {
                    new MetricEvent(countBeforeReset, countTags, timestamp, null, null, null),
                    new MetricEvent(GetAverage(snapshot, snapshotSize), avgTags, timestamp, config.Unit, null, null)
                };

                if (config.Quantiles != null)
                    for (var i = 0; i < config.Quantiles.Length; i++)
                        result.Add(new MetricEvent(GetQuantile(config.Quantiles[i], snapshot, snapshotSize), quantileTags[i], timestamp, config.Unit, null, null));

                return result;
            }
        }

        public void Dispose()
            => registration.Dispose();

        private static double GetAverage(double[] snapshot, int snapshotSize)
            => snapshotSize == 0 ? 0 : snapshot.Take(snapshotSize).Average();

        private static double GetQuantile(double quantile, double[] snapshot, int snapshotSize)
        {
            if (snapshotSize == 0)
                return 0;

            var position = quantile * (snapshotSize + 1);
            var index = (int)Math.Round(position);

            if (index < 1)
                return snapshot[0];

            if (index >= snapshotSize)
                return snapshot[snapshotSize - 1];

            return snapshot[index];
        }

        private static MetricTags[] PrepareQuantileTags(SummaryConfig config, MetricTags baseTags)
        {
            var quantileTags = new MetricTags[config.Quantiles.Length];

            for (var i = 0; i < config.Quantiles.Length; i++)
            {
                var quantileValue = config.Quantiles[i];
                if (quantileValue < 0d || quantileValue > 1d)
                    throw new ArgumentOutOfRangeException(nameof(config), $"One of provided quantiles has incorrect value '{quantileValue}'.");

                quantileValue *= 100d;

                while (Math.Abs(Math.Truncate(quantileValue) - quantileValue) > double.Epsilon)
                    quantileValue *= 10d;

                quantileTags[i] = baseTags.Append(WellKnownTagKeys.Aggregate, "p" + (int)quantileValue);
            }

            return quantileTags;
        }
    }
}