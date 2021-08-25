using JetBrains.Annotations;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public class GaugeConfig : ScrapableMetricConfig
    {
        /// <summary>
        /// Whether or not to send gauge with zero value.
        /// </summary>
        public bool SendZeroValues { get; set; } = true;
    }
}