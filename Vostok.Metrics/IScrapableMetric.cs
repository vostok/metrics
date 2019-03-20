using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// Represents a metric which state should be periodically observed.
    /// </summary>
    [PublicAPI]
    public interface IScrapableMetric
    {
        /// <summary>
        /// <para>Converts internal metric state to one or several <see cref="MetricSample">MetricSamples</see>.</para>
        /// <para>Implementation could reset the state after this method was called.</para>
        /// </summary>
        /// <returns><see cref="MetricSample">MetricSamples</see> describing current state of the metric</returns>
        IEnumerable<MetricSample> Scrape();
    }
}