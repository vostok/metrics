using System;
using Vostok.Metrics.Abstractions.Helpers;

namespace Vostok.Metrics.Abstractions.Primitives.TimingImpl
{
    public static class ITimingExtensions
    {
        public static void Report(this ITiming timing, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, timing.Unit);
            timing.Report(value);
        }
    }
}