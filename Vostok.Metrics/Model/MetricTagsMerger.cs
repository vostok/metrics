using Vostok.Metrics.WellKnownConstants;

namespace Vostok.Metrics.Model
{
    public static class MetricTagsMerger
    {
        public static MetricTags Merge(MetricTags contextTags, string name, MetricTags dynamicTags)
        {
            return contextTags
                .Add(MetricTagKeys.Name, name)
                .AddRange(dynamicTags);
        }

        public static MetricTags Merge(MetricTags contextTags, string name)
        {
            return contextTags
                .Add(MetricTagKeys.Name, name);
        }
    }
}