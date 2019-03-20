using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;
using Vostok.Metrics.WellKnownConstants;

namespace Vostok.Metrics.Primitives.CounterImpl
{
    internal class Counter : ICounter
    {
        private readonly IMetricContext context;
        private readonly MetricTags tags;
        private readonly CounterConfig config;

        public Counter([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] CounterConfig config)
        {
            this.context = context;
            this.tags = tags;
            this.config = config;
        }

        public void Add(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "Only values >= 0 can be added to counter",
                    nameof(value));
            }
            
            context.Send(new MetricEvent(
                value,
                DateTimeOffset.Now,
                config.Unit,
                AggregationTypes.Counter,
                tags));
        }

        public string Unit => config.Unit;

        public void Dispose() {}
    }
}