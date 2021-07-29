using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public static T Measure<T>([NotNull] this ITimer timer, Func<T> action)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return action();
            }
            finally
            {
                timer.Report(stopwatch.Elapsed);
            }
        }

        public static void Measure([NotNull] this ITimer timer, Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                action();
            }
            finally
            {
                timer.Report(stopwatch.Elapsed);
            }
        }

        public static async Task<T> Measure<T>([NotNull] this ITimer timer, Func<Task<T>> action)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return await action().ConfigureAwait(false);
            }
            finally
            {
                timer.Report(stopwatch.Elapsed);
            }
        }

        public static async Task Measure([NotNull] this ITimer timer, Func<Task> action)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await action().ConfigureAwait(false);
            }
            finally
            {
                timer.Report(stopwatch.Elapsed);
            }
        }

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