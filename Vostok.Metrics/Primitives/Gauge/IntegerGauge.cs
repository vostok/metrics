using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="IIntegerGauge"/>
    internal class IntegerGauge : IFastScrapableMetric, IIntegerGauge, IDisposable
    {
        private readonly MetricTags tags;
        private readonly IntegerGaugeConfig config;
        private readonly IDisposable registration;
        private long value;

        public IntegerGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] IntegerGaugeConfig config)
        {
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            value = config.InitialValue;
            registration = context.Register(this, config.ScrapePeriod);
        }

        public long CurrentValue => Interlocked.Read(ref value);

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            var valueToReport = config.ResetOnScrape
                ? Interlocked.Exchange(ref value, config.InitialValue)
                : CurrentValue;

            yield return new MetricEvent(valueToReport, tags, timestamp, config.Unit, null, null);
        }

        public void Set(long newValue)
            => Interlocked.Exchange(ref value, newValue);

        public void Increment()
            => Interlocked.Increment(ref value);

        public void Decrement()
            => Interlocked.Decrement(ref value);

        public void Add(long valueToAdd)
            => Interlocked.Add(ref value, valueToAdd);

        public void Substract(long valueToSubstract)
            => Add(-valueToSubstract);

        public void TryIncreaseTo(long candidateValue)
        {
            while (true)
            {
                var currentValue = CurrentValue;
                if (candidateValue <= currentValue || TrySet(candidateValue, currentValue))
                    return;
            }
        }

        public void TryReduceTo(long candidateValue)
        {
            while (true)
            {
                var currentValue = CurrentValue;
                if (candidateValue >= currentValue || TrySet(candidateValue, currentValue))
                    return;
            }
        }

        public void Dispose()
            => registration.Dispose();

        private bool TrySet(long newValue, long expectedValue)
            => Interlocked.CompareExchange(ref value, newValue, expectedValue) == expectedValue;
    }
}