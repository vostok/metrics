using System;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    [PublicAPI]
    public static class IHistogramExtensions
    {
        public static void Report(this IHistogram histogram, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, histogram.Unit);
            histogram.Report(value);
        }
    }
}