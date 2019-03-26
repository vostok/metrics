using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.Counter
{
    [PublicAPI]
    public class CounterConfig
    {
        internal static readonly CounterConfig Default = new CounterConfig();

        /// <summary>
        /// See <see cref="MetricEvent.Unit"/> and <see cref="WellKnownUnits"/> for more info.
        /// </summary>
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; }

        /// <summary>
        /// See <see cref="MetricEvent.AggregationParameters"/> for more info.
        /// </summary>
        [CanBeNull]
        public IReadOnlyDictionary<string, string> AggregationParameters { get; set; }
    }
}
