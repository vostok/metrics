using System;
using System.Collections.Concurrent;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Grouping
{
    internal abstract class MetricGroupBase<TMetric> : IDisposable
    {
        private readonly ConcurrentDictionary<ReadonlyInternalMetricTags, Lazy<TMetric>> cache = new ConcurrentDictionary<ReadonlyInternalMetricTags, Lazy<TMetric>>();
        private readonly Func<ReadonlyInternalMetricTags, TMetric> factory;

        protected MetricGroupBase([NotNull] Func<ReadonlyInternalMetricTags, TMetric> factory)
            => this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

        public void Dispose()
        {
            foreach (var pair in cache)
            {
                (pair.Value.Value as IDisposable)?.Dispose();

                cache.TryRemove(pair.Key, out _);
            }
        }

        protected TMetric For(ReadonlyInternalMetricTags dynamicTags)
            => cache.GetOrAdd(dynamicTags, t => new Lazy<TMetric>(() => factory(t), LazyThreadSafetyMode.ExecutionAndPublication)).Value;
    }
}