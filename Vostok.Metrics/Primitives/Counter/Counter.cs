using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Counter
{
    internal class Counter : ICounter, IScrapableMetric
    {
        private readonly MetricTags tags;
        private readonly CounterConfig config;
        private readonly IDisposable registration;

        private long counter;

        public Counter([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] CounterConfig config)
        {
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);
        }

        public void Add(long value)
        {
            if (value < 0L)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Only values >= 0 can be added to a counter.");

            Interlocked.Add(ref counter, value);
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            yield return new MetricEvent(Interlocked.Exchange(ref counter, 0), tags, timestamp, config.Unit, 
                WellKnownAggregationTypes.Counter, config.AggregationParameters);
        }

        public void Dispose()
            => registration.Dispose();
    }
}
