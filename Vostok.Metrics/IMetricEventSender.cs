using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    public interface IMetricEventSender
    {
        void Send(MetricEvent @event);
    }
}