using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public class HistogramConfig : ScrapableMetricConfig
    {
        internal static readonly HistogramConfig Default = new HistogramConfig();

        public HistogramConfig()
        {
            Unit = WellKnownUnits.Seconds;
            ScrapeOnDispose = false;
        }

        [NotNull]
        public HistogramBuckets Buckets { get; set; }
            = new HistogramBuckets(.005, .01, .025, .05, .075, .1, .25, .5, .75, 1, 2.5, 5, 7.5, 10, 60);

        /// <summary>
        /// See <see cref="MetricEvent.AggregationParameters"/> for more info.
        /// </summary>
        [CanBeNull]
        public IReadOnlyDictionary<string, string> AggregationParameters { get; set; }
    }
}