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

        // TODO(iloktionov): implement default attribute-based extraction of tags from object fields and properties
        public TMetric For(TFor value)
        {
            throw new NotImplementedException();
        }
    }
}