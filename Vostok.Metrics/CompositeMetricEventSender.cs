using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    [PublicAPI]
    public class CompositeMetricSampleSender : IMetricSampleSender
    {
        private readonly IMetricSampleSender[] senders;

        public CompositeMetricSampleSender(IMetricSampleSender[] senders)
        {
            this.senders = senders;
        }

        public void Send(MetricSample sample)
        {
            foreach (var sender in senders)
            {
                sender.Send(sample);
            }
        }
    }
}