using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// <para>Represents configuration of a <see cref="MetricContext"/> instance.</para>
    /// <para></para>
    /// </summary>
    [PublicAPI]
    public class MetricContextConfig
    {
        public MetricContextConfig([NotNull] IMetricEventSender sender)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        /// <summary>
        /// Sender used to offload <see cref="MetricEvent"/>s for further processing.
        /// </summary>
        [NotNull]
        public IMetricEventSender Sender { get; }

        /// <summary>
        /// A set of tags inherent to every metric produced with configured context instance.
        /// </summary>
        [CanBeNull]
        public MetricTags Tags { get; set; }

        /// <summary>
        /// Default metric scrape period (used when passing a <c>null</c> period to <see cref="IMetricContext.Register"/> method).
        /// </summary>
        public TimeSpan DefaultScrapePeriod { get; set; } = TimeSpan.FromMinutes(1);
    }
}
