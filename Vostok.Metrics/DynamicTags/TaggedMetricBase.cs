using System;
using System.Collections.Concurrent;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags
{
    internal abstract class TaggedMetricBase<TMetric> : IDisposable
    {
        private readonly ConcurrentDictionary<MetricTags, TMetric> cache = new ConcurrentDictionary<MetricTags, TMetric>();
        private readonly Func<MetricTags, TMetric> factory;

        protected TaggedMetricBase(Func<MetricTags, TMetric> factory)
        {
            this.factory = factory;
        }

        protected TMetric For(MetricTags dynamicTags)
        { 
            return cache.GetOrAdd(dynamicTags, factory);
        }

        public void Dispose()
        {
            //todo Should guarantee that all TMetrics are disposed and no new TMetrics are created after Dispose 
            foreach (var kvp in cache)
            {
                (kvp.Value as IDisposable)?.Dispose();
            }
        }
    }
}