using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="IFloatingGauge"/>
    internal class FloatingGauge : IScrapableMetric, IFloatingGauge
    {
        private readonly MetricTags tags;
        private readonly FloatingGaugeConfig config;
        private readonly IDisposable registration;
        private double value;

        public FloatingGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] FloatingGaugeConfig config)
        {
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            value = config.InitialValue;
            registration = context.Register(this, config.ScrapePeriod);
        }

        public double CurrentValue => Interlocked.CompareExchange(ref value, config.InitialValue, config.InitialValue);

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            var valueToReport = config.ResetOnScrape
                ? Interlocked.Exchange(ref value, config.InitialValue)
                : CurrentValue;

            yield return new MetricEvent(valueToReport, tags, timestamp, config.Unit, null, null);
        }

        public void Set(double newValue)
            => Interlocked.Exchange(ref value, newValue);

        public void Substract(double valueToSubstract)
            => Add(-valueToSubstract);

        public void Add(double valueToAdd)
        {
            while (true)
            {
                var currentValue = value;
                var newValue = currentValue + valueToAdd;

                if (Math.Abs(Interlocked.CompareExchange(ref value, newValue, currentValue) - currentValue) < double.Epsilon * 100)
                    return;
            }
        }

        public void Dispose()
            => registration.Dispose();
    }
}
