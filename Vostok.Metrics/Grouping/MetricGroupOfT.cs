using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Grouping
{
    internal class MetricGroup<TFor, TMetric> : MetricGroupBase<TMetric>, IMetricGroup<TFor, TMetric>
    {
        public MetricGroup(Func<MetricTags, TMetric> factory)
            : base(factory)
        {
        }

        public TMetric For(TFor value)
        {
            throw new NotImplementedException();
        }
    }
}