using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Commons.Threading;
using Vostok.Metrics.Models;
using Signal = Vostok.Commons.Threading.AsyncManualResetEvent;

namespace Vostok.Metrics.Scraping
{
    /// <summary>
    /// Small transparent wrapper for underlying <see cref="IScrapableMetric"/> which controls disposal safety.
    /// </summary>
    internal class SafeScrapableMetric : IScrapableMetric, IDisposable
    {
        private readonly Signal scrapeEnded = new Signal(true);
        private readonly AtomicBoolean disposed = new AtomicBoolean(false);
        private readonly IScrapableMetric metric;

        public SafeScrapableMetric([NotNull] IScrapableMetric metric)
        {
            this.metric = metric;
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            scrapeEnded.Reset();

            if (!disposed)
                foreach (var metricEvent in metric.Scrape(timestamp))
                    yield return metricEvent;

            scrapeEnded.Set();
        }

        public void Dispose()
        {
            if (!disposed.TrySetTrue())
                scrapeEnded.WaitAsync().GetAwaiter().GetResult();
        }

        #region EqualityMembers

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is SafeScrapableMetric otherSafeScrapable)
                return metric.Equals(otherSafeScrapable.metric);
            if (obj is IScrapableMetric otherScrapable)
                return metric.Equals(otherScrapable);
            return false;
        }

        public override int GetHashCode() =>
            metric.GetHashCode();

        #endregion
    }
}