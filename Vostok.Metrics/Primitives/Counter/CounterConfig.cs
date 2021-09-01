using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Counter
{
    [PublicAPI]
    public class CounterConfig : ScrapableMetricConfig
    {
        internal static readonly CounterConfig Default = new CounterConfig();

        public CounterConfig()
        {
            ScrapeOnDispose = true;
        }

        /// <summary>
        /// See <see cref="MetricEvent.AggregationParameters"/> for more info.
        /// </summary>
        [CanBeNull]
        public IReadOnlyDictionary<string, string> AggregationParameters { get; set; }

        /// <summary>
        /// Whether or not to send counter with zero value.
        /// </summary>
        public bool SendZeroValues { get; set; } = true;
    }
}