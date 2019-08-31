using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Vostok.Metrics.Primitives.Caching
{
    internal class PerContextCache
    {
        private readonly ConcurrentDictionary<(string name, Type type), Lazy<object>> items =
            new ConcurrentDictionary<(string name, Type type), Lazy<object>>();

        public object Obtain(string name, Type type, Func<object> factory)
            => items.GetOrAdd((name, type), _ => new Lazy<object>(factory, LazyThreadSafetyMode.ExecutionAndPublication)).Value;
    }
}
