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
            foreach (var pair in cache)
            {
                (pair.Value as IDisposable)?.Dispose();

                cache.TryRemove(pair.Key, out _);
            }
        }
    }
}