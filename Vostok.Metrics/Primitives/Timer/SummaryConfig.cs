using JetBrains.Annotations;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public class SummaryConfig : ScrapableMetricConfig
    {
        internal static readonly SummaryConfig Default = new SummaryConfig();

        public SummaryConfig()
        {
            Unit = WellKnownUnits.Seconds;
        }

        /// <summary>
        /// Size of internal buffer used to store a sample of incoming values.
        /// </summary>
        public int BufferSize { get; set; } = 1028;

        /// <summary>
        /// <para>A set of quantiles to compute. Each quantile must be a number from 0 to 1.</para>
        /// <para>If <c>null</c>, <see cref="Primitives.Timer.Quantiles.DefaultQuantiles"/> will be used.</para>
        /// </summary>
        public double[] Quantiles { get; set; }
    }
}