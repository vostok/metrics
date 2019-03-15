using System.Collections.Generic;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl
{
    public interface IHistogram
    {
        void Report(double value);
    }
}