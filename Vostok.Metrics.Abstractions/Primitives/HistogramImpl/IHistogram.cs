using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Primitives.HistogramImpl
{
    public interface IHistogram : IDisposable
    {
        void Report(double value);
        [CanBeNull] string Unit { get; }
    }
}