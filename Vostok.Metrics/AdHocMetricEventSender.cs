using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    [PublicAPI]
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