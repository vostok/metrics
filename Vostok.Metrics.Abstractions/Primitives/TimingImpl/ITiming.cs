using System;

namespace Vostok.Metrics.Abstractions.Primitives.TimingImpl
{
    public interface ITiming : IDisposable
    {
        void Report(double value);
    }
}