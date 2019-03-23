using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    /// <summary>
    /// <para><see cref="MetricEvent"/> is the atomic storage unit of Vostok metrics system.</para>
    /// <para>Every event contains a numeric <see cref="Value"/> measured at some <see cref="Timestamp"/> and bound to a set of <see cref="Tags"/>.</para>
    /// <para>Events may also contain auxiliary information, such as <see cref="Unit"/> and <see cref="AggregationType"/>.</para>
    /// <para><see cref="MetricEvent"/> instances are immutable. Consider using <see cref="MetricEventBuilder"/> to construct them.</para>
    /// </summary>
    [PublicAPI]
    public class MetricEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">See <see cref="Value"/>.</param>
        /// <param name="tags">See <see cref="Tags"/>.</param>
        /// <param name="timestamp">See <see cref="Timestamp"/>.</param>
        /// <param name="unit">See <see cref="Unit"/>.</param>
        /// <param name="aggregationType">See <see cref="AggregationType"/>.</param>
        public MetricEvent(
            double value,
            MetricTags tags,
            DateTimeOffset timestamp,
            [CanBeNull] [ValueProvider("Vostok.Metrics.WellKnownUnits")]
            string unit,
            [CanBeNull] [ValueProvider("Vostok.Metrics.WellKnownAggregationTypes")]
            string aggregationType)
        {
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));

            if (tags.Count == 0)
                throw new ArgumentException("Empty tags are not allowed in metric events.", nameof(tags));

            Unit = unit;
            Value = value;
            Timestamp = timestamp;
            AggregationType = aggregationType;
        }

        /// <summary>
        /// Numeric value of the metric represented by this <see cref="MetricEvent"/>.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Timestamp denoting the moment when this <see cref="MetricEvent"/>'s <see cref="Value"/> was observed.
        /// </summary>
        public DateTimeOffset Timestamp { get; }

        /// <summary>
        /// <para>Set of key-value string tags this event's <see cref="Value"/> is bound to. May not be empty.</para>
        /// <para>See <see cref="MetricTags"/> for more details.</para>
        /// </summary>
        [NotNull]
        public MetricTags Tags { get; }

        /// <summary>
        /// <para>Unit of measurement this event's <see cref="Value"/> is expressed in.</para>
        /// <para>Dimensionless quantities (such as count) may have no unit (<c>null</c> value).</para>
        /// <para>See <see cref="WellKnownUnits"/> for predefined unit constants.</para>
        /// </summary>
        [CanBeNull]
        public string Unit { get; }

        /// <summary>
        /// <para>Aggregation type defines what happens to the event after it's constructed and sent.</para>
        /// <para><c>Null</c> value means no aggregation: event will be published to the metrics storage of choice (like Graphite or Prometheus) as is.</para>
        /// <para>Any non-null value implies server-side processing: event will undergo some kind of aggregation that will eventually produce other events with <c>null</c> <see cref="AggregationType"/>.</para>
        /// <para>See <see cref="WellKnownAggregationTypes"/> for predefined type constants.</para>
        /// </summary>
        [CanBeNull]
        public string AggregationType { get; }
    }
}
