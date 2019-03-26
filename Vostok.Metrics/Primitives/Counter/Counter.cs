using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.Counter
{
    internal class Counter : ICounter
    {
        private readonly IMetricContext context;
        private readonly MetricTags tags;
        private readonly CounterConfig config;

        public Counter([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] CounterConfig config)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Add(long value)
        {
            if (value < 0L)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Only values >= 0 can be added to counter");
            
            context.Send(new MetricEvent(value, tags, DateTimeOffset.Now, config.Unit, WellKnownAggregationTypes.Counter, null));
        }
    }
}