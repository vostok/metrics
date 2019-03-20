using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    public class MetricContext : IMetricContext
    {
        private readonly IMetricEventSender sender;
        private readonly MetricContextConfig config;
        private readonly ScrapeScheduler scrapeScheduler;
        
        public MetricContext(IMetricEventSender sender, MetricContextConfig config = null)
        {
            this.sender = sender;
            this.config = config ?? MetricContextConfig.Default;
            scrapeScheduler = new ScrapeScheduler(ScrapeAction);
        }

        public MetricTags Tags => config.Tags;
        public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod)
        {
            return scrapeScheduler.Register(metric, scrapePeriod ?? config.DefaultScrapePeriod);
        }

        public void Send(MetricEvent @event)
        {
            sender.Send(@event);
        }

        private void ScrapeAction(IScrapableMetric metric, TimeSpan scrapePeriod, DateTimeOffset scrapeTimestamp)
        {
            foreach (var metricEvent in metric.Scrape())
            {
                Send(metricEvent);
            }
        }
    }
}