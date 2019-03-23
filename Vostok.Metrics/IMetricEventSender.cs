using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// Sends <see cref="MetricEvent"/> for further aggregation or to a permanent storage.
    /// Implementations are expected to be thread-safe and exception-free.
    /// </summary>
    [PublicAPI]
    public interface IMetricSampleSender
    {
        /// <inheritdoc cref="IMetricSampleSender"/>
        void Send(MetricEvent @event);
    }
}