using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    [PublicAPI]
    public class CompositeMetricEventSender : IMetricEventSender
    {
        private readonly IMetricEventSender[] senders;

        public CompositeMetricEventSender(IMetricEventSender[] senders)
        {
            this.senders = senders;
        }

        public void Send(MetricEvent @event)
        {
            foreach (var sender in senders)
            {
                sender.Send(@event);
            }
        }
    }
}