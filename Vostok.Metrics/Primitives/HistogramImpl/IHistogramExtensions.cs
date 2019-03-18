using System;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.HistogramImpl
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