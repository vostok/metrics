using System;
using System.Collections.Concurrent;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags
{
    // auto-generate all TaggedMetricN<T> classes
    // this class is public because it is used from the impl library
    public class TaggedMetric2<TMetric> : ITaggedMetric2<TMetric>
    {
        protected readonly ConcurrentDictionary<MetricTags, TMetric> cache = new ConcurrentDictionary<MetricTags, TMetric>();
        private readonly Func<MetricTags, TMetric> factory;
        
        private readonly string key1;
        private readonly string key2;

        public TaggedMetric2(Func<MetricTags, TMetric> factory, string key1, string key2)
        {
            this.factory = factory;
            this.key1 = key1;
            this.key2 = key2;
        }

        public TMetric For(string value1, string value2)
        {
            var tag1 = new MetricTag(key1, value1);
            var tag2 = new MetricTag(key2, value2);
            var tags = new MetricTags(); // add tag1, tag2

            return cache.GetOrAdd(tags, factory);
        }
    }
}