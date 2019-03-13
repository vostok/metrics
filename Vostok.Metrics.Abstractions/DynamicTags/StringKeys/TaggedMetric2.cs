using System;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags.StringKeys
{
    // auto-generate all TaggedMetricN<T> classes
    public class TaggedMetric2<TMetric> : TaggedMetricBase<TMetric>, ITaggedMetric2<TMetric>
    {
        private readonly string key1;
        private readonly string key2;

        public TaggedMetric2(Func<MetricTags, TMetric> factory, string key1, string key2) : base(factory)
        {
            this.key1 = key1;
            this.key2 = key2;
        }

        public TMetric For(string value1, string value2)
        {
            var tag1 = new MetricTag(key1, value1);
            var tag2 = new MetricTag(key2, value2);
            var tags = new MetricTags(); // add tag1, tag2

            return For(tags);
        }
    }
}