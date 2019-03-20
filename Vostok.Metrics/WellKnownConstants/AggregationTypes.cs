using JetBrains.Annotations;

namespace Vostok.Metrics.WellKnownConstants
{
    [PublicAPI]
    public static class AggregationTypes
    {
        public const string Timing = "timing";
        public const string Histogram = "histogram";
    }
}