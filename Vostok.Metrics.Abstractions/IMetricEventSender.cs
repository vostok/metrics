using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions
{
    public interface IMetricEventSender
    {
        void Send(MetricEvent @event);
    }
}