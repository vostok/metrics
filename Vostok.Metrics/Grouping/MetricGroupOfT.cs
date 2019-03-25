using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Grouping
{
    internal class MetricGroup<TFor, TMetric> : MetricGroupBase<TMetric>, IMetricGroup<TFor, TMetric>
    {
        private readonly ITypeTagsConverter<TFor> converter;

        public MetricGroup(Func<MetricTags, TMetric> factory, ITypeTagsConverter<TFor> converter = null)
            : base(factory)
        {
            this.converter = converter ?? TypeTagsConverter<TFor>.Default;
        }

        public TMetric For(TFor value)
        {
            var tags = converter.Convert(value);
            return For(tags);
        }
    }
}