using System;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl
{
    internal class Timing : ITiming
    {
        private readonly IMetricContext context;
        private readonly TimingConfig config;

        public Timing(IMetricContext context, MetricTags tags, TimingConfig config)
        {
            this.context = context;
            this.config = config;
        }

        public void Report(double value)
        {
            var metricEvent = new MetricEvent(value, DateTimeOffset.Now, config.Unit, config.AggregationType, context.Tags);
            context.Send(metricEvent);
        }
    }
}