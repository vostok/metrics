using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Gauge;

namespace Vostok.Metrics.Primitives.Reporter
{
    /// <summary>
    /// <para>
    /// Reporter is a fairly straightforward primitive that simply allows to report metric values at arbitrary time.
    /// It is essentially a convenience wrapper around <see cref="IMetricContext.Send"/> method.
    /// </para>
    /// <para>
    /// Reporter assumes no further aggregation of metric events.
    /// </para>
    /// <para>
    /// Reporters are not subject to periodical scraping: every <see cref="Report"/> call immediately produces and sends a metric event.
    /// </para>
    /// <para>
    /// Reporters are lightweight and stateless: they can be created and discarded as needed.
    /// </para>
    /// <para>
    /// To create an instance of <see cref="IReporter"/>, use <see cref="ReporterFactoryExtensions"/>.
    /// </para>
    /// <example>
    /// <para>
    /// Use a reporter to implement batch metrics production at intervals suitable for your application
    /// when it's not feasible to support an <see cref="IFuncGauge"/> for each metric independently.
    /// </para>
    /// </example>
    /// </summary>
    [PublicAPI]
    public interface IReporter
    {
        /// <summary>
        /// Constructs a <see cref="MetricEvent"/> with given <paramref name="value"/> and sends it immediately.
        /// </summary>
        void Report(double value);
    }
}
