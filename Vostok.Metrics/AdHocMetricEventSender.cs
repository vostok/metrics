using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// Delegates <see cref="Send"/> implementation to a custom action
    /// </summary>
    [PublicAPI]
    public class AdHocMetricSampleSender : IMetricSampleSender
    {
        private readonly Action<MetricEvent> sendAction;

        /// <param name="sendAction">
        /// This will be called every time <see cref="Send"/> occurs.
        /// The delegate should be thread-safe and exception-free.
        /// </param>
        public AdHocMetricSampleSender(Action<MetricEvent> sendAction)
        {
            this.sendAction = sendAction;
        }

        public void Send(MetricEvent @event)
        {
            sendAction(@event);
        }
    }
}