using System;

namespace Vostok.Metrics
{
    internal class ScrapeScheduler
    {
        private readonly Action<IScrapableMetric, TimeSpan, DateTimeOffset> scrapeAction;

        public ScrapeScheduler(Action<IScrapableMetric, TimeSpan, DateTimeOffset> scrapeAction)
        {
            this.scrapeAction = scrapeAction;
        }

        public IDisposable Register(IScrapableMetric metric, TimeSpan scrapePeriod)
        {
            throw new NotImplementedException();
        }
        
        private void Unregister(IScrapableMetric metric)
        {
            throw new NotImplementedException();
        }

        private class Registration : IDisposable
        {
            private readonly ScrapeScheduler scheduler;
            private readonly IScrapableMetric metric;

            public Registration(ScrapeScheduler scheduler, IScrapableMetric metric)
            {
                this.scheduler = scheduler;
                this.metric = metric;
            }

            public void Dispose()
            {
                scheduler.Unregister(metric);
            }
        }
    }
}