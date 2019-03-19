using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    public static class MetricTagsExtensions
    {
        public static MetricTags Add(
            this MetricTags tags, 
            [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricTagKeys")] string key,
            string value)
        {
            return tags.Add(new MetricTag(key, value));
        }
    }
}