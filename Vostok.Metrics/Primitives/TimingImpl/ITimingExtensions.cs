using System;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    [PublicAPI]
    public static class ITimingExtensions
    {
        public static void Report(this ITiming timing, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, timing.Unit);
            timing.Report(value);
        }
    }
}