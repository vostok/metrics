using System;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl
{
    public interface ITiming : IDisposable
    {
        void Report(double value);
    }
}