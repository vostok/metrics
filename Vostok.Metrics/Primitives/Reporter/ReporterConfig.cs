using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Reporter
{
    [PublicAPI]
    public class ReporterConfig
    {
        internal static readonly ReporterConfig Default = new ReporterConfig();

        /// <summary>
        /// See <see cref="MetricEvent.Unit"/> and <see cref="WellKnownUnits"/> for more info.
        /// </summary>
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; }

        /// <summary>
        /// An optional common timestamp for all <see cref="MetricEvent"/>s produced by an instance of <see cref="IReporter"/>.
        /// </summary>
        [CanBeNull]
        public DateTimeOffset? Timestamp { get; set; }
    }
}