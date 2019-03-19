using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    public class MetricEvent
    {
        public double Value { get; }
        
        public DateTimeOffset Timestamp { get; }
        
        [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricUnits")]
        public string Unit { get; }
        
        [ValueProvider("Vostok.Metrics.WellKnownConstants.AggregationTypes")]
        public string AggregationType { get; }
        
        public MetricTags Tags { get; }

        public MetricEvent(
            double value,
            DateTimeOffset timestamp,
            [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricUnits")] string unit,
            [ValueProvider("Vostok.Metrics.WellKnownConstants.AggregationTypes")] string aggregationType,
            MetricTags tags)
        {
            Value = value;
            Timestamp = timestamp;
            Unit = unit;
            AggregationType = aggregationType;
            Tags = tags;
        }
    }
}