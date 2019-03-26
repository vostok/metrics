using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;
using Vostok.Metrics.Scheduler;

namespace Vostok.Metrics
{
    /// <inheritdoc cref="IMetricContext"/>
    [PublicAPI]
    public class MetricContext : IMetricContext, IDisposable
    {
        private readonly IMetricEventSender sender;
        private readonly MetricContextConfig config;
        private readonly ScrapeScheduler scrapeScheduler;
        
        public MetricContext([NotNull] MetricContextConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            scrapeScheduler = new ScrapeScheduler(ScrapeAction);
        }

        public MetricTags Tags => config.Tags ?? MetricTags.Empty;

        public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod)
            => scrapeScheduler.Register(metric, scrapePeriod ?? config.DefaultScrapePeriod);

        public void Send(MetricEvent @event)
            => sender.Send(@event);

        private void ScrapeAction(IScrapableMetric metric, TimeSpan scrapePeriod, DateTimeOffset scrapeTimestamp)
        {
            var currentTime = DateTimeOffset.Now;

            foreach (var metricSample in metric.Scrape(currentTime))
            {
                Send(metricSample);
            }
        }

        public void Dispose()
            => scrapeScheduler.Dispose();
    }
}