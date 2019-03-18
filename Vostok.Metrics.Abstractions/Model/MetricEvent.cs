using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Model
{
    public class MetricEvent
    {
        public double Value { get; }
        
        public DateTimeOffset Timestamp { get; }
        
        [ValueProvider("Vostok.Metrics.Abstractions.MetricUnits")]
        public string Unit { get; }
        
        [ValueProvider("Vostok.Metrics.Abstractions.AggregationTypes")]
        public string AggregationType { get; }
        
        public MetricTags Tags { get; }

        public MetricEvent(double value, DateTimeOffset timestamp, string unit, string aggregationType, MetricTags tags)
        {
            Value = value;
            Timestamp = timestamp;
            Unit = unit;
            AggregationType = aggregationType;
            Tags = tags;
        }
    }
}