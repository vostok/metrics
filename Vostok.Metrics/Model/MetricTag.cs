namespace Vostok.Metrics.Model
{
    public class MetricTag
    {
        public string Key { get; }
        public string Value { get; }

        public MetricTag(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}