using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    public interface ITiming : IDisposable
    {
        void Report(double value);
        [CanBeNull] string Unit { get; }
    }
}