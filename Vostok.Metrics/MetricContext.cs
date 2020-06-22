using System;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Counter;
using Vostok.Metrics.Scraping;
using Vostok.Metrics.Senders;

namespace Vostok.Metrics
{
    /// <inheritdoc cref="IMetricContext"/>
    [PublicAPI]
    public class MetricContext : IMetricContext, IDisposable
    {
        private static volatile IMetricEventSender[] globalSenders = Array.Empty<IMetricEventSender>();

        private readonly MetricContextConfig config;
        private readonly IMetricEventSender sender;
        private readonly ScrapeScheduler scheduler;
        private readonly ScrapeScheduler fastScheduler;
        private readonly ScrapeScheduler scrapeOnDisposeScheduler;

        public MetricContext([NotNull] MetricContextConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            sender = new CompositeDynamicMetricEventSender(config.Sender, () => globalSenders);
            scheduler = new ScrapeScheduler(sender, config.ErrorCallback);
            fastScheduler = new ScrapeScheduler(sender, config.ErrorCallback);
            scrapeOnDisposeScheduler = new ScrapeScheduler(sender, config.ErrorCallback, true);
        }

        public static void AddGlobalSender([NotNull] IMetricEventSender sender)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            var oldGlobalSenders = globalSenders;
            var newGlobalSenders = new IMetricEventSender[oldGlobalSenders.Length + 1];

            Array.Copy(oldGlobalSenders, newGlobalSenders, oldGlobalSenders.Length);

            newGlobalSenders[oldGlobalSenders.Length] = sender;

            Interlocked.Exchange(ref globalSenders, newGlobalSenders);
        }

        public MetricTags Tags => config.Tags ?? MetricTags.Empty;

        public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod) =>
            GetScheduler(metric)
                .Register(metric, scrapePeriod ?? config.DefaultScrapePeriod);

        public void Send(MetricEvent @event)
            => sender.Send(@event);

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
