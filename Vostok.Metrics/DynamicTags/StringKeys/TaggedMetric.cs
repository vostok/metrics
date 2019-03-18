using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags.StringKeys
{
    //design auto-generate all TaggedMetricN<T> classes
    public class StringKeysTaggedMetric<TMetric> : TaggedMetricBase<TMetric>,
        ITaggedMetric1<TMetric>,
        ITaggedMetric2<TMetric>,
        ITaggedMetric3<TMetric>
    {
        private readonly string[] keys;

        public StringKeysTaggedMetric(IMetricContext context, Func<MetricTags, TMetric> factory, params string[] keys)
            : base(context, factory)
        {
            this.keys = keys;
        }

        public StringKeysTaggedMetric(IMetricContext context, Func<MetricTags, TMetric> factory, TimeSpan? scrapePeriod, params string[] keys)
            : base(context, factory, scrapePeriod)
        {
            this.keys = keys;
        }

        public TMetric For(string value1)
        {
            var tag1 = new MetricTag(keys[0], value1);
            var tags = new MetricTags(); // add tag1
            
            return For(tags);
        }
        
        public TMetric For(string value1, string value2)
        {
            var tag1 = new MetricTag(keys[0], value1);
            var tag2 = new MetricTag(keys[1], value2);
            var tags = new MetricTags(); // add tag1, tag2

            return For(tags);
        }
        
        public TMetric For(string value1, string value2, string value3)
        {
            var tag1 = new MetricTag(keys[0], value1);
            var tag2 = new MetricTag(keys[1], value2);
            var tag3 = new MetricTag(keys[2], value3);
            var tags = new MetricTags(); // add tag1, tag2, tag3

            return For(tags);
        }
    }
}