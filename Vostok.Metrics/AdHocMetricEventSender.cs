using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    public class AdHocMetricEventSender : IMetricEventSender
    {
        private readonly Action<MetricEvent> sendAction;

        public AdHocMetricEventSender(Action<MetricEvent> sendAction)
        {
            this.sendAction = sendAction;
        }

        public void Send(MetricEvent @event)
        {
            sendAction(@event);
        }
    }
}