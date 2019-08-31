using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using Vostok.Commons.Collections;

namespace Vostok.Metrics.Primitives.Caching
{
    internal static class GlobalCache
    {
        private static readonly ConcurrentDictionary<IMetricContext, PerContextCache> PerContextCaches =
            new ConcurrentDictionary<IMetricContext, PerContextCache>(ByReferenceEqualityComparer<IMetricContext>.Instance);

        public static TMetric Obtain<TMetric>([NotNull] IMetricContext context, [NotNull] string name, [NotNull] Func<TMetric> factory)
            => (TMetric)PerContextCaches.GetOrAdd(context, _ => new PerContextCache()).Obtain(name, typeof(TMetric), () => factory());
    }
}
