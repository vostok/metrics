using System;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.Wrappers
{
    internal class MetricContextTagWrapper : IMetricContext
    {
        private readonly IMetricContext context;

        public MetricContextTagWrapper(IMetricContext context, MetricTags tags)
        {
            this.context = context;
            Tags = tags;
        }

        public MetricTags Tags { get; }

        public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod)
        {
            return context.Register(metric, scrapePeriod);
        }

        public void Send(MetricEvent @event)
        {
            context.Send(@event);
        }
    }
}