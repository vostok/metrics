using System;
using System.Collections.Concurrent;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Grouping
{
    internal abstract class MetricGroupBase<TMetric> : IDisposable
    {
        private readonly ConcurrentDictionary<MetricTags, Lazy<TMetric>> cache = new ConcurrentDictionary<MetricTags, Lazy<TMetric>>();
        private readonly Func<MetricTags, Lazy<TMetric>> factory;

        protected MetricGroupBase([NotNull] Func<MetricTags, TMetric> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            this.factory = t => { return new Lazy<TMetric>(() => factory(t), LazyThreadSafetyMode.ExecutionAndPublication); };
        }

        public void Dispose()
        {
            foreach (var pair in cache)
            {
                (pair.Value.Value as IDisposable)?.Dispose();

                cache.TryRemove(pair.Key, out _);
            }
        }

        protected TMetric For([NotNull] MetricTags dynamicTags)
            => cache.GetOrAdd(dynamicTags, factory).Value;
    }
}