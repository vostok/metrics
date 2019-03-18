using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    public interface IGauge : IDisposable
    {
        void Set(double value);
        void Add(double value);
        [CanBeNull] string Unit { get; }
    }
}