using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public class HistogramConfig
    {
        internal static readonly HistogramConfig Default = new HistogramConfig();

        [CanBeNull]
        public double[] Buckets { get; set; }

        /// <summary>
        /// See <see cref="MetricEvent.Unit"/> and <see cref="WellKnownUnits"/> for more info.
        /// </summary>
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; } = WellKnownUnits.Time.Seconds;

        /// <summary>
        /// See <see cref="MetricEvent.AggregationParameters"/> for more info.
        /// </summary>
        [CanBeNull]
        public IReadOnlyDictionary<string, string> AggregationParameters { get; set; }

        /// <summary>
        /// Period of scraping gauge's current value. If left <c>null</c>, context default period will be used.
        /// </summary>
        [CanBeNull]
        public TimeSpan? ScrapePeriod { get; set; }
    }
}
