using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public class SummaryConfig
    {
        internal static readonly SummaryConfig Default = new SummaryConfig();

        /// <summary>
        /// See <see cref="MetricEvent.Unit"/> and <see cref="WellKnownUnits"/> for more info.
        /// </summary>
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; } = WellKnownUnits.Seconds;

        /// <summary>
        /// Period of scraping summary's current value. If left <c>null</c>, context default period will be used.
        /// </summary>
        [CanBeNull]
        public TimeSpan? ScrapePeriod { get; set; }

        /// <summary>
        /// Size of internal buffer used to store a sample of incoming values.
        /// </summary>
        public int BufferSize { get; set; } = 1028;

        /// <summary>
        /// A set of quantiles to compute. Each quantile must be a number from 0 to 1.
        /// </summary>
        public double[] Quantiles = { 0.5, 0.75, 0.95, 0.99, 0.999 };
    }
}
