using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics
{
    /// <summary>
    /// A metric event sender that passes metric events to all of the base metric event senders.
    /// </summary>
    [PublicAPI]
    public class CompositeMetricEventSender : IMetricEventSender
    {
        private readonly IMetricEventSender[] baseMetricEventSenders;

        public CompositeMetricEventSender(params IMetricEventSender[] baseMetricEventSenders)
        {
            this.baseMetricEventSenders = baseMetricEventSenders ?? throw new ArgumentNullException(nameof(baseMetricEventSenders));
        }

        public void Send(MetricEvent @event)
        {
            foreach (var baseMetricEventSender in baseMetricEventSenders)
            {
                baseMetricEventSender.Send(@event);
            }
        }
    }
}