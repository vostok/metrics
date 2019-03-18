using System;

namespace Vostok.Metrics.Abstractions.Primitives.HistogramImpl
{
    public interface IHistogram : IDisposable
    {
        void Report(double value);
    }
}