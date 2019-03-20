using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags.Typed
{
    internal class TaggedMetricT<TFor, TMetric> : TaggedMetricBase<TMetric>, ITaggedMetricT<TFor, TMetric>
    {
        private readonly ITypeTagsConverter<TFor> converter;

        public TaggedMetricT(Func<MetricTags, TMetric> factory, ITypeTagsConverter<TFor> converter = null)
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