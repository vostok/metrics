using System;
using System.Collections.Generic;
using Vostok.Metrics.Abstractions.DynamicTags;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    internal class FuncGauge : IScrapableMetric
    {
        private readonly Func<double> getValue;
        private readonly MetricTags tags;
        private readonly GaugeConfig config;

        public FuncGauge(Func<double> getValue, MetricTags tags, GaugeConfig config)
        {
            this.getValue = getValue;
            this.tags = tags;
            this.config = config;
        }

        public IEnumerable<MetricEvent> Scrape()
        {
            var value = getValue();
            var result = new MetricEvent(
                value,
                DateTimeOffset.UtcNow,
                config.Unit,
                config.AggregationType,
                tags);
            yield return result;
        }
    }
}