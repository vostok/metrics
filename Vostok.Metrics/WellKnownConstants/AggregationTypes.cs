using JetBrains.Annotations;

namespace Vostok.Metrics.WellKnownConstants
{
    [PublicAPI]
    public static class AggregationTypes
    {
        public const string Timer = "timer";
        public const string Histogram = "histogram";
        public static string Counter = "counter";
    }
}