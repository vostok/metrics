using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public static class IGaugeExtensions
    {
        public static void Inc(this IGauge gauge)
        {
            gauge.Add(1);
        }
        
        public static void Dec(this IGauge gauge)
        {
            gauge.Add(-1);
        }

        public static void Subtract(this IGauge gauge, double value)
        {
            gauge.Add(-value);
        }
    }
}