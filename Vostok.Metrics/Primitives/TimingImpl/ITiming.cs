using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    [PublicAPI]
    public interface ITiming : IDisposable
    {
        void Report(double value);
        [CanBeNull] string Unit { get; }
    }
}