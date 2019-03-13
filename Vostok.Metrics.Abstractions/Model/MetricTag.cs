namespace Vostok.Metrics.Abstractions.Model
{
    public class MetricTag
    {
        public readonly string Key;
        public readonly string Value;

        public MetricTag(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}