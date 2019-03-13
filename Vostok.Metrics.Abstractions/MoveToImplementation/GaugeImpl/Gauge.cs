using System;
using System.Collections.Generic;
using System.Threading;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    public class Gauge : IScrapableMetric, IGauge
    {
        private double value = 0;
        private readonly MetricTags tags;
        private readonly GaugeConfig config;

        public Gauge(MetricTags tags, GaugeConfig config)
        {
            this.tags = tags;
            this.config = config;
        }

        public IEnumerable<MetricEvent> Scrape()
        {
            yield return new MetricEvent(value, DateTimeOffset.Now, config.Unit, config.AggregationType, tags);
        }

        public void Set(double value)
        {
            Interlocked.Exchange(ref this.value, value);
        }

        public void Inc()
        {
            Add(1);
        }

        public void Dec()
        {
            Add(-1);
        }

        public void Subtract(double value)
        {
            Add(-value);
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
    }
}