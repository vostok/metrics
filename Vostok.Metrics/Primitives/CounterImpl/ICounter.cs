using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.CounterImpl
{
    [PublicAPI]
    public interface ICounter : IDisposable
    {
        void Add(double value);
        string Unit { get; }
    }
}