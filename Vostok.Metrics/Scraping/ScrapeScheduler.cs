using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Commons.Helpers.Disposable;
using Vostok.Commons.Helpers.Extensions;

namespace Vostok.Metrics.Scraping
{
    internal class ScrapeScheduler
    {
        private readonly IMetricEventSender sender;
        private readonly Action<Exception> errorCallback;
        private readonly bool scrapeOnDispose;
        private readonly CancellationTokenSource cancellation;
        private readonly ConcurrentDictionary<TimeSpan, ScrapableMetrics> scrapableMetrics;
        private readonly ConcurrentDictionary<TimeSpan, Task> scraperTasks;

        public ScrapeScheduler(IMetricEventSender sender, Action<Exception> errorCallback, bool scrapeOnDispose = false)
        {
            this.sender = sender;
            this.errorCallback = errorCallback;
            this.scrapeOnDispose = scrapeOnDispose;

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
                scraperTasks[scrapePeriod] = new Scraper(sender, errorCallback)
                    .RunAsync(metrics, scrapePeriod, cancellation.Token)
                    .SilentlyContinue();
            }

            return new ActionDisposable(
                () =>
                {
                    metrics.Remove(metric);

                    // todo (kungurtsev, 07.12.2021): 
                    // 1. obtain metric Scraper
                    // 2. wait when Scraper to finish current iteration
                    
                    ScrapeOnDispose(metric);
                });
        }

        public void Dispose()
        {
            cancellation.Cancel();

            Task.WhenAll(scraperTasks.Values)
                .GetAwaiter()
                .GetResult();

            ScrapeOnDispose();

            scraperTasks.Clear();
            scrapableMetrics.Clear();
        }

        private void ScrapeOnDispose()
        {
            if (!scrapeOnDispose)
                return;

            new Scraper(sender, errorCallback)
                .ScrapeOnce(scrapableMetrics.SelectMany(m => m.Value));
        }

        private void ScrapeOnDispose(IScrapableMetric metric)
        {
            if (!scrapeOnDispose)
                return;

            new Scraper(sender, errorCallback)
                .ScrapeOnce(new[] {metric});
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
    }
}