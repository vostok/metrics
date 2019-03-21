using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    [PublicAPI]
    public static class IHistogramExtensionsMeasure
    {
        public static void Report(this IHistogram metric, TimeSpan timeSpan)
        {
            var value = TimeSpanToDoubleConverter.ConvertOrThrow(timeSpan, metric.Unit);
            metric.Report(value);
        }

        public static IDisposable Measure(this IHistogram metric)
        {
            return new Measurement(metric);
        }
        
        private class Measurement : IDisposable
        {
            private readonly Stopwatch stopwatch;
            private readonly IHistogram metric;
            
            public Measurement(IHistogram metric)
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
