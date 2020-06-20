using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Senders
{
    /// <summary>
    /// Prints incoming <see cref="MetricEvent"/>s to text and executes an arbitrary delegate on them.
    /// </summary>
    [PublicAPI]
    public class PrintedMetricEventSender : IMetricEventSender
    {
        private readonly Action<string> send;
        private readonly MetricEventPrintFormat format;

        public PrintedMetricEventSender([NotNull] Action<string> send, MetricEventPrintFormat format)
        {
            this.send = send ?? throw new ArgumentNullException(nameof(send));
            this.format = format;
        }

        public void Send(MetricEvent @event)
            => send(MetricEventPrinter.Print(@event, format));
    }
}
