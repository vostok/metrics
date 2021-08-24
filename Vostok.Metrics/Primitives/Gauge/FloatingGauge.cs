using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="IFloatingGauge"/>
    internal class FloatingGauge : IFastScrapableMetric, IFloatingGauge, IDisposable
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

            registration = context.Register(this, config);
        }

        public double CurrentValue => Interlocked.CompareExchange(ref value, config.InitialValue, config.InitialValue);

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            var valueToReport = config.ResetOnScrape
                ? Interlocked.Exchange(ref value, config.InitialValue)
                : CurrentValue;

            if (valueToReport != 0 || config.SendZeroValues)
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
                if (TrySet(currentValue + valueToAdd, currentValue))
                    return;
            }
        }

        public void TryIncreaseTo(double candidateValue)
        {
            while (true)
            {
                var currentValue = CurrentValue;
                if (candidateValue <= currentValue || TrySet(candidateValue, currentValue))
                    return;
            }
        }

        public void TryReduceTo(double candidateValue)
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

        private bool TrySet(double newValue, double expectedValue)
            => Math.Abs(Interlocked.CompareExchange(ref value, newValue, expectedValue) - expectedValue) < double.Epsilon * 100;
    }
}