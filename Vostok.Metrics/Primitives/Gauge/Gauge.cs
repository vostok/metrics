using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="IGauge"/>
    internal class Gauge : IScrapableMetric, IGauge
    {
        private readonly MetricTags tags;
        private readonly GaugeConfig config;
        private readonly IDisposable registration;
        private double value;

        public Gauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] GaugeConfig config)
        {
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            value = config.InitialValue;
            registration = context.Register(this, config.ScrapePeriod);
        }

        // TODO(iloktionov): make efficient for integers
        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            var valueToReport = config.ResetOnScrape
                ? Interlocked.Exchange(ref value, config.InitialValue)
                : Interlocked.CompareExchange(ref value, config.InitialValue, config.InitialValue);

            yield return new MetricEvent(valueToReport, tags, timestamp, config.Unit, null, null);
        }

        public void Set(double newValue)
            => Interlocked.Exchange(ref value, newValue);

        public void Add(double valueToAdd)
        {
            while (true)
            {
                var currentValue = value;
                var newValue = currentValue + valueToAdd;

                if (Interlocked.CompareExchange(ref value, newValue, currentValue) == currentValue)
                    return;
            }
        }

        public void Dispose()
            => registration.Dispose();
    }
}
