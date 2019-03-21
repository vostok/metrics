using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    [PublicAPI]
    public static class ITimingExtensionsMeasure
    {
        public static void Report(this ITiming metric, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, metric.Unit);
            metric.Report(value);
        }

        public static IDisposable Measure(this ITiming metric)
        {
            return new Measurement(metric);
        }
        
        private class Measurement : IDisposable
        {
            private readonly Stopwatch stopwatch;
            private readonly ITiming metric;
            
            public Measurement(ITiming metric)
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
