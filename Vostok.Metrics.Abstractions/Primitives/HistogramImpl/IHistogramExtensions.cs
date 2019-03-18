using System;
using Vostok.Metrics.Abstractions.Helpers;

namespace Vostok.Metrics.Abstractions.Primitives.HistogramImpl
{
    public static class IHistogramExtensions
    {
        public static void Report(this IHistogram histogram, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, histogram.Unit);
            histogram.Report(value);
        }
    }
}