using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Counter;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics
{
    /// <inheritdoc cref="IMetricContext"/>
    [PublicAPI]
    public class MetricContext : IMetricContext, IDisposable
    {
        private readonly MetricContextConfig config;
        private readonly ScrapeScheduler scheduler;
        private readonly ScrapeScheduler fastScheduler;
        private readonly ScrapeScheduler scrapeOnDisposeScheduler;

        public MetricContext([NotNull] MetricContextConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            scheduler = new ScrapeScheduler(config.Sender, config.ErrorCallback);
            fastScheduler = new ScrapeScheduler(config.Sender, config.ErrorCallback);
            scrapeOnDisposeScheduler = new ScrapeScheduler(config.Sender, config.ErrorCallback, true);
        }

        public MetricTags Tags => config.Tags ?? MetricTags.Empty;

        public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod) =>
            GetScheduler(metric)
                .Register(metric, scrapePeriod ?? config.DefaultScrapePeriod);

        public void Send(MetricEvent @event)
            => config.Sender.Send(@event);

        public void Dispose()
        {
            scheduler.Dispose();
            fastScheduler.Dispose();
            scrapeOnDisposeScheduler.Dispose();
        }

        private ScrapeScheduler GetScheduler(IScrapableMetric metric)
        {
            if (metric is ICounter)
                return scrapeOnDisposeScheduler;

            if (metric is IFastScrapableMetric)
                return fastScheduler;

            return scheduler;
        }
    }
}