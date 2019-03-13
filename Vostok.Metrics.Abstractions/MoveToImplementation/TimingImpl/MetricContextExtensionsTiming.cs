using System;
using Vostok.Metrics.Abstractions.DynamicTags;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl
{
    public static class MetricContextExtensionsTiming
    {
        public static ITiming Timing(this IMetricContext context, string name, TimingConfig config = null)
        {
            config = config ?? TimingConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            var metric = new Timing(context, tags, config);
            return metric;
        }

        public static ITaggedMetric2<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, TimingConfig config = null)
        {
            config = config ?? TimingConfig.Default;
            var taggedMetric = new TaggedMetric2<Timing>(tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                var metric = new Timing(context, finalTags, config);
                return metric;
            }, key1, key2);
            
            return taggedMetric;
        }
    }
}