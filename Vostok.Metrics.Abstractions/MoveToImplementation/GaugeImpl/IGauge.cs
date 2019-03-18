using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    public interface IGauge : IDisposable
    {
        void Set(double value);
        void Add(double value);
        [CanBeNull] string Unit { get; }
    }
}