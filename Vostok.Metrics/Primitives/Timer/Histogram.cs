using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

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
    /// Call <see cref="IDisposable.Dispose"/> to stop scraping the metric.
    /// </para>
    /// </remarks>
    internal class Histogram : ITimer, IScrapableMetric
    {
        private readonly MetricTags tags;
        private readonly HistogramConfig config;
        private readonly IDisposable registration;

        public Histogram([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] HistogramConfig config)
        {
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);
        }

        public string Unit => config.Unit;

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            throw new NotImplementedException();
        }

        public void Report(double value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
            => registration.Dispose();
    }
}
