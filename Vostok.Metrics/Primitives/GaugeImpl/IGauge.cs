using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    [PublicAPI]
    public interface IGauge : IDisposable
    {
        void Set(double value);
        void Add(double value);
        [CanBeNull] string Unit { get; }
    }
}