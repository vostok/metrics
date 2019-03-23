using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    [PublicAPI]
    public static class MetricTagsMerger
    {
        public static MetricTags Merge(MetricTags contextTags, string name, MetricTags dynamicTags)
        {
            return contextTags
                .Add(WellKnownTagKeys.Name, name)
                .AddRange(dynamicTags);
        }

        public static MetricTags Merge(MetricTags contextTags, string name)
        {
            return contextTags
                .Add(WellKnownTagKeys.Name, name);
        }
    }
}