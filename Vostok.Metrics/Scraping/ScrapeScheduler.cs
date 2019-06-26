using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Commons.Helpers.Extensions;

namespace Vostok.Metrics.Scraping
{
    internal class ScrapeScheduler
    {
        private readonly IMetricEventSender sender;
        private readonly Action<Exception> errorCallback;
        private readonly CancellationTokenSource cancellation;
        private readonly ConcurrentDictionary<TimeSpan, ScrapableMetrics> scrapableMetrics;
        private readonly ConcurrentDictionary<TimeSpan, Task> scraperTasks;

        public ScrapeScheduler(IMetricEventSender sender, Action<Exception> errorCallback)
        {
            this.sender = sender;
            this.errorCallback = errorCallback;

            cancellation = new CancellationTokenSource();
            scraperTasks = new ConcurrentDictionary<TimeSpan, Task>();
            scrapableMetrics = new ConcurrentDictionary<TimeSpan, ScrapableMetrics>();
        }

        public IDisposable Register(IScrapableMetric metric, TimeSpan scrapePeriod)
        {
            if (scrapePeriod <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(scrapePeriod), scrapePeriod, "Scrape period must be positive.");

            var (metrics, created) = ObtainMetricsForPeriod(scrapePeriod);

            metrics.Add(metric);

            if (created)
            {
                scraperTasks[scrapePeriod] = new Scraper(sender, errorCallback, scrapePeriod)
                    .RunAsync(metrics, cancellation.Token)
                    .SilentlyContinue();
            }

            return new Registration(metric, metrics);
        }

        public void Dispose()
        {
            cancellation.Cancel();

            Task.WhenAll(scraperTasks.Values)
                .GetAwaiter()
                .GetResult();

            scraperTasks.Clear();
            scrapableMetrics.Clear();
        }

        private (ScrapableMetrics metrics, bool created) ObtainMetricsForPeriod(TimeSpan scrapePeriod)
        {
            if (scrapableMetrics.TryGetValue(scrapePeriod, out var metrics))
                return (metrics, false);

            var newMetrics = new ScrapableMetrics();

            if (scrapableMetrics.TryAdd(scrapePeriod, newMetrics))
                return (newMetrics, true);

            return (scrapableMetrics[scrapePeriod], false);
        }

        private class Registration : IDisposable
        {
            private readonly IScrapableMetric metric;
            private readonly ScrapableMetrics metrics;

            public Registration(IScrapableMetric metric, ScrapableMetrics metrics)
            {
                this.metric = metric;
                this.metrics = metrics;
            }

            public void Dispose()
                => metrics.Remove(metric);
        }
    }
}