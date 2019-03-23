using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// Names for some of well-recognized measurement <see cref="MetricEvent.Unit">units</see>.
    /// </summary>
    [PublicAPI]
    public static class WellKnownUnits
    {
        [PublicAPI]
        public static class Time
        {
            public const string Ticks = "ticks";
            public const string Nanoseconds = "nanoseconds";
            public const string Microseconds = "microseconds";
            public const string Milliseconds = "milliseconds";
            public const string Seconds = "seconds";
            public const string Minutes = "minutes";
            public const string Hours = "hours";
            public const string Days = "days";
        }

        [PublicAPI]
        public static class OperationRate
        {
            public const string OpsPerSecond = "ops/second";
            public const string OpsPerMinute = "ops/minute";
        }

        [PublicAPI]
        public static class DataSize
        {
            public const string Bytes = "bytes";
            public const string Kilobytes = "kilobytes";
            public const string Megabytes = "megabytes";
            public const string Gigabytes = "gigabytes";
            public const string Terabytes = "terabytes";
            public const string Petabytes = "petabytes";
            public const string Exabytes = "exabytes";

            public const string Bits = "bits";
            public const string Kilobits = "kilobits";
            public const string Megabits = "megabits";
            public const string Gigabits = "gigabits";
            public const string Terabits = "terabits";
            public const string Petabits = "petabits";
            public const string Exabits = "exabits";
        }

        [PublicAPI]
        public static class DataRate
        {
            public static readonly string BytesPerSecond = $"{DataSize.Bytes}/second";
            public static readonly string KilobytesPerSecond = $"{DataSize.Kilobytes}/second";
            public static readonly string MegabytesPerSecond = $"{DataSize.Megabytes}/second";
            public static readonly string GigabytesPerSecond = $"{DataSize.Gigabytes}/second";
            public static readonly string TerabytesPerSecond = $"{DataSize.Terabytes}/second";
            public static readonly string PetabytesPerSecond = $"{DataSize.Petabytes}/second";
            public static readonly string ExabytesPerSecond = $"{DataSize.Exabytes}/second";

            public static readonly string BitsPerSecond = $"{DataSize.Bits}/second";
            public static readonly string KilobitsPerSecond = $"{DataSize.Kilobits}/second";
            public static readonly string MegabitsPerSecond = $"{DataSize.Megabits}/second";
            public static readonly string GigabitsPerSecond = $"{DataSize.Gigabits}/second";
            public static readonly string TerabitsPerSecond = $"{DataSize.Terabits}/second";
            public static readonly string PetabitsPerSecond = $"{DataSize.Petabits}/second";
            public static readonly string ExabitsPerSecond = $"{DataSize.Exabits}/second";
        }
    }
}
