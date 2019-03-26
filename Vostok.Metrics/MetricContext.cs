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
        
        /// <inheritdoc cref="IMetricContext"/>
        /// <param name="sender">Calls to <see cref="Send"/> will be delegated to the specified <see cref="IMetricEventSender"/></param>
        /// <param name="config">Optional config. Use it to specify parameters common for all metric primitives in this <see cref="IMetricContext"/></param>
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
            foreach (var metricSample in metric.Scrape())
            {
                Send(metricSample);
            }
        }

        public void Dispose()
        {
            scrapeScheduler.Dispose();
        }
    }
}