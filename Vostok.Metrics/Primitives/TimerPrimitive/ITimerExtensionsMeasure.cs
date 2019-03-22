using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.TimerPrimitive
{
    [PublicAPI]
    public static class ITimerExtensionsMeasure
    {
        public static void Report(this ITimer metric, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, metric.Unit);
            metric.Report(value);
        }

        public static IDisposable Measure(this ITimer metric)
        {
            return new Measurement(metric);
        }
        
        private class Measurement : IDisposable
        {
            private readonly Stopwatch stopwatch;
            private readonly ITimer metric;
            
            public Measurement(ITimer metric)
            {
                this.metric = metric;
                //todo Maybe use PreciseDateTime here?
                stopwatch = Stopwatch.StartNew();
            }

            public void Dispose()
            {
                metric.Report(stopwatch.Elapsed);
            }
        }
    }
}
