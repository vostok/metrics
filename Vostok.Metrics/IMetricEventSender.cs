using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    [PublicAPI]
    public interface IMetricEventSender
    {
        void Send(MetricEvent @event);
    }
}