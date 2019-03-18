using System;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl
{
    public interface IHistogram : IDisposable
    {
        void Report(double value);
    }
}