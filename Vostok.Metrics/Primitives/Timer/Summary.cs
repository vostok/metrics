using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    /// You configure Summary by specifying desired memory usage or error margins.
    /// An online algorithm then computes quantiles on the stream of reported values.
    /// Internal state is periodically scraped to obtain metrics.
    /// </para>
    /// <para>
    /// Summaries are aggregated client-side.
    /// Output of aggregation includes total count, sum and approximate percentiles.
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
        private readonly MetricTags tags;
        private readonly SummaryConfig config;
        private readonly IDisposable registration;

        public Summary([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] SummaryConfig config)
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
        {
            throw new NotImplementedException();
        }
    }
}