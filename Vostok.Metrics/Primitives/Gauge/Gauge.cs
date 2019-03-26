using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.Gauge
{
    internal class Gauge : IScrapableMetric, IGauge
    {
        private double value = 0;
        private readonly MetricTags tags;
        private readonly GaugeConfig config;
        private readonly IDisposable registration;

        public Gauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] GaugeConfig config)
        {
            this.tags = tags;
            this.config = config;
            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricEvent> Scrape()
        {
            yield return new MetricEvent(value, tags, DateTimeOffset.Now, config.Unit, null, null);
        }

        public void Set(double value)
        {
            Interlocked.Exchange(ref this.value, value);
        }

        public void Add(double value)
        {
            while (true)
            {
                var curValue = this.value;
                var newValue = curValue + value;

                var actualCurValue = Interlocked.CompareExchange(ref this.value, newValue, curValue);
                if (actualCurValue.Equals(curValue))
                {
                    return;
                }
            }
        }

        public void Dispose()
        {
            registration.Dispose();
        }
    }
}