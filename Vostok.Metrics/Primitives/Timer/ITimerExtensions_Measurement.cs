using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public static class ITimerExtensions_Measurement
    {
        /// <inheritdoc cref="ITimer.Report"/>
        public static void Report([NotNull] this ITimer timer, TimeSpan value)
            => timer.Report(TimeValuesConverter.ConvertOrThrow(value, timer.Unit));

        public static IDisposable Measure([NotNull] this ITimer timer)
            => new Measurement(timer);
        
        private class Measurement : Stopwatch, IDisposable
        {
            private readonly ITimer timer;
            
            public Measurement(ITimer timer)
            {
                this.timer = timer;

                Start();
            }

            public void Dispose()
            {
                timer.Report(Elapsed);
            }
        }
    }
}
