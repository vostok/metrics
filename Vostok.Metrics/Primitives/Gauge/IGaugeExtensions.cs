using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public static class IGaugeExtensions
    {
        public static void Increment([NotNull] this IGauge gauge)
            => gauge.Add(1);
        
        public static void Decrement([NotNull] this IGauge gauge)
            => gauge.Add(-1);

        public static void Subtract([NotNull] this IGauge gauge, double value)
            => gauge.Add(-value);
    }
}