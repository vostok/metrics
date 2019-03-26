using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// Represents a metric whose state can be periodically observed.
    /// </summary>
    [PublicAPI]
    public interface IScrapableMetric
    {
        /// <summary>
        /// <para>Converts internal metric state to one or more <see cref="MetricEvent"/>s.</para>
        /// <para>Implementation could reset the state after this method was called.</para>
        /// </summary>
        /// <returns><see cref="MetricEvent"/>s describing current state of the metric.</returns>
        [NotNull]
        [ItemNotNull]
        IEnumerable<MetricEvent> Scrape();
    }
}