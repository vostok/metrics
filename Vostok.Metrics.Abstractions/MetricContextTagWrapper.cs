using System;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions
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

        public IDisposable Register(IScrapableMetric metric, TimeSpan scrapePeriod) =>
            context.Register(metric, scrapePeriod);

        public void Send(MetricEvent @event)
        {
            context.Send(@event);
        }
    }
}