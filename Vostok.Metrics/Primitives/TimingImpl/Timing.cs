using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;
using Vostok.Metrics.WellKnownConstants;

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
            var MetricSample = new MetricSample(
                value,
                tags,
                DateTimeOffset.Now,
                config.Unit,
                AggregationTypes.Timing);
            context.Send(MetricSample);
        }

        public string Unit => config.Unit;

        public void Dispose()
        {
        }
    }
}