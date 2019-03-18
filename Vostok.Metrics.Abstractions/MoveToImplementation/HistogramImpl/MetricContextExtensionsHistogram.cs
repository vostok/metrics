using Vostok.Metrics.Abstractions.DynamicTags.StringKeys;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl
{
    public static class MetricContextExtensionsHistogram
    {
        public static IHistogram Histogram(this IMetricContext context, string name, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Histogram(context, tags, config);
        }

        private static StringKeysTaggedMetric<Histogram> CreateStringKeysTaggedHistogram(IMetricContext context, string name, HistogramConfig config = null, params string[] keys)
        {
            config = config ?? HistogramConfig.Default;
            return new StringKeysTaggedMetric<Histogram>(
                context, 
                tags =>
                {
                    var finalTags1 = MetricTagsMerger.Merge(context.Tags, name, tags);
                    return new Histogram(context, finalTags1, config);
                },
                config.ScrapePeriod,
                keys);
        }
        
        public static ITaggedMetric1<IHistogram> Histogram(this IMetricContext context, string name, string key1, HistogramConfig config = null)
        {
            return CreateStringKeysTaggedHistogram(context, name, config, key1);
        }
        
        public static ITaggedMetric2<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, HistogramConfig config = null)
        {
            return CreateStringKeysTaggedHistogram(context, name, config, key1, key2);
        }
        
        public static ITaggedMetric3<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, HistogramConfig config = null)
        {
            return CreateStringKeysTaggedHistogram(context, name, config, key1, key2, key3);
        }
    }
}