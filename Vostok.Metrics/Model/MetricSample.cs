using System;
using JetBrains.Annotations;
using Vostok.Metrics.Primitives.CounterPrimitive;

namespace Vostok.Metrics.Model
{
    /// <summary>
    /// <para>
    /// Represents a single <see cref="Value"/> scraped from the metric primitive
    /// and a user's intention (see <see cref="AggregationType"/>) to do something with this value.
    /// </para> 
    /// </summary>
    [PublicAPI]
    public class MetricSample
    {
        /// <summary>
        /// The value of this MetricSample 
        /// </summary>
        public double Value { get; }
        
        /// <summary>
        /// When the <see cref="Value"/> was observed.
        /// </summary>
        public DateTimeOffset Timestamp { get; }
        
        /// <summary>
        /// <para>The unit of the <see cref="Value"/>.</para>
        /// <para>See well-known units in <see cref="WellKnownUnits"/></para>
        /// </summary>
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; }
        
        /// <summary>
        /// <para>AggregationType defines what will happen with the MetricSample.</para>
        /// <para>
        /// If AggregationType is <c>null</c> the sample should be saved as-is to a persistent storage like Graphite
        /// </para>
        /// <para>
        /// Otherwise, the sample will undergo some aggregation process.
        /// For example, samples produced by <see cref="ICounter"/> primitive
        /// should be summed up server-side before saving.
        /// </para>
        /// <para>
        /// See well-known aggregation types in <see cref="WellKnownAggregationTypes"/>
        /// </para>
        /// </summary>
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownAggregationTypes")]
        public string AggregationType { get; }
        
        /// <summary>
        /// <inheritdoc cref="MetricTags"/>
        /// </summary>
        public MetricTags Tags { get; }

        /// <summary>
        /// <inheritdoc cref="MetricSample"/>
        /// <para>You may also use <see cref="MetricSampleBuilder"/> to create a MetricSample</para>
        /// </summary>
        /// <param name="value">Observed value</param>
        /// <param name="tags">Collection of tags describing the value. See <see cref="Tags"/></param>
        /// <param name="timestamp">When the value was observed</param>
        /// <param name="unit">Unit of the value</param>
        /// <param name="aggregationType">What to do with the value. See <see cref="AggregationType"/></param>
        public MetricSample(
            double value,
            MetricTags tags,
            DateTimeOffset timestamp,
            [ValueProvider("Vostok.Metrics.WellKnownUnits")]
            string unit,
            [ValueProvider("Vostok.Metrics.WellKnownAggregationTypes")]
            string aggregationType)
        {
            Value = value;
            Timestamp = timestamp;
            Unit = unit;
            AggregationType = aggregationType;
            Tags = tags;
        }
    }
}