using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Commons.Time;

namespace Vostok.Metrics.Scraping
{
    internal class Scraper
    {
        private static readonly TimeSpan RolloverWaitPause = TimeSpan.FromMilliseconds(1);

        private readonly IMetricEventSender sender;
        private readonly Action<Exception> errorCallback;
        private readonly TimeSpan period;

        public Scraper(IMetricEventSender sender, Action<Exception> errorCallback, TimeSpan period)
        {
            this.sender = sender;
            this.errorCallback = errorCallback;
            this.period = period;
        }

        public async Task RunAsync(ScrapedMetrics metrics, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var initialTimestamp = Now;

                var delayToNextScrape = GetDelayToNextScrape(initialTimestamp, period);
                if (delayToNextScrape > TimeSpan.Zero)
                    await Task.Delay(delayToNextScrape, cancellationToken).ConfigureAwait(false);

                var thresholdTimestamp = initialTimestamp + delayToNextScrape;
                var scrapeTimestamp = await WaitForTimestampRollover(thresholdTimestamp, cancellationToken).ConfigureAwait(false);

                foreach (var metric in metrics)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    try
                    {
                        foreach (var metricEvent in metric.Scrape(scrapeTimestamp))
                            sender.Send(metricEvent);
                    }
                    catch (Exception error)
                    {
                        try
                        {
                            errorCallback?.Invoke(error);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
            }
        }

        private static DateTime Now => PreciseDateTime.UtcNow.UtcDateTime;

        private static TimeSpan GetDelayToNextScrape(DateTime now, TimeSpan period)
            => period - TimeSpan.FromTicks(now.Ticks % period.Ticks);

        private static async Task<DateTime> WaitForTimestampRollover(DateTime threshold, CancellationToken cancellationToken)
        {
            while (true)
            {
                var scrapeTimestamp = Now;
                if (scrapeTimestamp >= threshold)
                    return scrapeTimestamp;

                await Task.Delay(RolloverWaitPause, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
