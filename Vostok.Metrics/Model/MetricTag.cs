using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    public class MetricTag
    {
        [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricTagKeys")]
        public string Key { get; }
        
        public string Value { get; }

        public MetricTag(
            [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricTagKeys")] string key,
            string value)
        {
            Key = key;
            Value = value;
        }
    }
}