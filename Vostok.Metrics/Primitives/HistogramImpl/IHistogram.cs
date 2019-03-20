using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    [PublicAPI]
    public interface IHistogram : IDisposable
    {
        void Report(double value);
        [CanBeNull] string Unit { get; }
    }
}