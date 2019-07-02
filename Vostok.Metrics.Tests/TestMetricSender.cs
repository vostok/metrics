using System.Collections.Generic;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Tests
{
    internal class TestMetricSender : IMetricEventSender
    {
        public List<MetricEvent> Events = new List<MetricEvent>();

        public void Send(MetricEvent @event)
        {
            lock (Events)
            {
                Events.Add(@event);
            }
        }
    }
}