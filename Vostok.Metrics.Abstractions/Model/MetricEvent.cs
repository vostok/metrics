using System;

namespace Vostok.Metrics.Abstractions.Model
{
    public class MetricEvent
    {
        public readonly double Value;
        public readonly DateTimeOffset Timestamp;
        public readonly string Unit;
        public readonly string AggregationType;
        public readonly MetricTags Tags;

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