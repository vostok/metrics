using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    internal class Timing : ITiming
    {
        private readonly IMetricContext context;
        private readonly MetricTags tags;
        private readonly TimingConfig config;

        public Timing([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] TimingConfig config)
        {
            this.context = context;
            this.tags = tags;
            this.config = config;
        }

        public void Report(double value)
        {
            var metricEvent = new MetricEvent(
                value,
                DateTimeOffset.Now,
                config.Unit,
                AggregationTypes.Timing,
                tags);
            context.Send(metricEvent);
        }

        public string Unit => config.Unit;

        public void Dispose()
        {
        }
    }
}