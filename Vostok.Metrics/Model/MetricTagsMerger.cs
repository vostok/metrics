using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    [PublicAPI]
    public static class MetricTagsMerger
    {
        public static MetricTags Merge(MetricTags contextTags, string name, MetricTags dynamicTags)
        {
            return contextTags
                .Append(WellKnownTagKeys.Name, name)
                .Append(dynamicTags);
        }

        public static MetricTags Merge(MetricTags contextTags, string name)
        {
            return contextTags
                .Append(WellKnownTagKeys.Name, name);
        }
    }
}