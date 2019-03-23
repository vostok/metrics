using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    [PublicAPI]
    public static class MetricTagsExtensions
    {
        public static MetricTags Add(
            this MetricTags tags, 
            [ValueProvider("Vostok.Metrics.WellKnownTagKeys")] string key,
            string value)
        {
            return tags.Add(new MetricTag(key, value));
        }
    }
}