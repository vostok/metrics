using System;
using Vostok.Metrics.Abstractions.DynamicTags;
using Vostok.Metrics.Abstractions.DynamicTags.StringKeys;
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

        private static StringKeysTaggedMetric<Timing> CreateStringKeysTaggedMetric(IMetricContext context, string name, TimingConfig config, params string[] keys)
        {
            config = config ?? TimingConfig.Default;
            var taggedMetric = new StringKeysTaggedMetric<Timing>(
                tags =>
                {
                    var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                    var metric = new Timing(context, finalTags, config);
                    return metric;
                },
                keys);
            return taggedMetric;
        }

        public static ITaggedMetric1<ITiming> Timing(this IMetricContext context, string name, string key1, TimingConfig config = null)
        {
            return CreateStringKeysTaggedMetric(context, name, config, key1);
        }
        
        public static ITaggedMetric2<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, TimingConfig config = null)
        {
            return CreateStringKeysTaggedMetric(context, name, config, key1, key2);
        }
        
        public static ITaggedMetric3<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, TimingConfig config = null)
        {
            return CreateStringKeysTaggedMetric(context, name, config, key1, key2, key3);
        }
    }
}