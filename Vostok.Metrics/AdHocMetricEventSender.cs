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
        private readonly Action<MetricSample> sendAction;

        /// <param name="sendAction">
        /// This will be called every time <see cref="Send"/> occurs.
        /// The delegate should be thread-safe.
        /// </param>
        public AdHocMetricSampleSender(Action<MetricSample> sendAction)
        {
            this.sendAction = sendAction;
        }

        public void Send(MetricSample sample)
        {
            sendAction(sample);
        }
    }
}