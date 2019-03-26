using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Timer
{
    internal static class TimeValuesConverter
    {
        public static double ConvertOrThrow(TimeSpan value, [CanBeNull] string unit)
        {
            switch (unit)
            {
                case WellKnownUnits.Time.Ticks:
                    return value.Ticks;

                case WellKnownUnits.Time.Microseconds:
                    return value.TotalMilliseconds / 1000d;

                case WellKnownUnits.Time.Milliseconds:
                    return value.TotalMilliseconds;

                case WellKnownUnits.Time.Seconds:
                    return value.TotalSeconds;

                case WellKnownUnits.Time.Minutes:
                    return value.TotalMinutes;

                case WellKnownUnits.Time.Hours:
                    return value.TotalMinutes;

                case WellKnownUnits.Time.Days:
                    return value.TotalDays;
            }

            throw new ArgumentException($"Cannot convert a TimeSpan value to '{unit}' unit.");
        }
    }
}