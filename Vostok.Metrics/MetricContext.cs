using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Gauge;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics
{
    /// <inheritdoc cref="IMetricContext"/>
    [PublicAPI]
    public class MetricContext : IMetricContext, IDisposable
    {
        private readonly MetricContextConfig config;
        private readonly ScrapeScheduler scheduler;
        private readonly ScrapeScheduler funcScheduler;

        public MetricContext([NotNull] MetricContextConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            scheduler = new ScrapeScheduler(config.Sender, config.ErrorCallback);
            funcScheduler = new ScrapeScheduler(config.Sender, config.ErrorCallback);
        }

        public MetricTags Tags => config.Tags ?? MetricTags.Empty;

        public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod)
            => (metric is FuncGauge ? funcScheduler : scheduler)
                .Register(metric, scrapePeriod ?? config.DefaultScrapePeriod);

        public void Send(MetricEvent @event)
            => config.Sender.Send(@event);

        public void Dispose()
        {
            scheduler.Dispose();
            funcScheduler.Dispose();
        }
    }
}