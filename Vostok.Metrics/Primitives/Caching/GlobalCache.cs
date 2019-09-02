using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Caching
{
    internal static class GlobalCache
    {
        private static readonly ConcurrentDictionary<WeakKey, PerContextCache> PerContextCaches =
            new ConcurrentDictionary<WeakKey, PerContextCache>(new WeakKeyComparer());

        public static TMetric Obtain<TMetric>([NotNull] IMetricContext context, [NotNull] string name, [CanBeNull] object details, [NotNull] Func<TMetric> factory)
            => (TMetric)PerContextCaches.GetOrAdd(new WeakKey(context), _ => new PerContextCache()).Obtain(name, typeof(TMetric), details, () => factory());

        public static void Cleanup()
        {
            foreach (var pair in PerContextCaches)
            {
                if (!pair.Key.Reference.IsAlive)
                    PerContextCaches.TryRemove(pair.Key, out _);
            }
        }

        #region WeakKey

        private struct WeakKey
        {
            public WeakReference Reference { get; }

            public int HashCode { get; }

            public WeakKey(IMetricContext context)
            {
                Reference = new WeakReference(context);
                HashCode = RuntimeHelpers.GetHashCode(context);
            }
        }

        #endregion

        #region WeakKeyComparer

        private class WeakKeyComparer : IEqualityComparer<WeakKey>
        {
            public int GetHashCode(WeakKey key) => key.HashCode;

            public bool Equals(WeakKey x, WeakKey y)
                => ReferenceEquals(x.Reference.Target, y.Reference.Target);
        }
        
        #endregion
    }
}
