using System;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags.Typed
{
    internal class TaggedMetricT<TMetric, TFor> : TaggedMetricBase<TMetric>, ITaggedMetricT<TMetric, TFor>
    {
        private readonly ITypeTagsConverter<TFor> converter;

        public TaggedMetricT(Func<MetricTags, TMetric> factory, ITypeTagsConverter<TFor> converter)
            : base(factory)
        {
            this.converter = converter;
        }

        public TMetric For(TFor value)
        {
            var tags = converter.Convert(value);
            return For(tags);
        }
    }
}