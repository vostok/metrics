using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Grouping
{
    internal abstract class MetricGroupBase<TMetric> : IDisposable
    {
        private readonly ConcurrentDictionary<MetricTags, TMetric> cache = new ConcurrentDictionary<MetricTags, TMetric>();
        private readonly Func<MetricTags, TMetric> factory;

        protected MetricGroupBase([NotNull] Func<MetricTags, TMetric> factory)
            => this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

        protected TMetric For([NotNull] MetricTags dynamicTags)
            => cache.GetOrAdd(dynamicTags, factory);

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