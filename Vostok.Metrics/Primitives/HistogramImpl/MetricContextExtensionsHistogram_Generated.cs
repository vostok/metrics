using Vostok.Metrics.DynamicTags.StringKeys;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    public static partial class MetricContextExtensionsHistogram
    {
        public static ITaggedMetric1<IHistogram> Histogram(this IMetricContext context, string name, string key1, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }
        public static ITaggedMetric2<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }
        public static ITaggedMetric3<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }
        public static ITaggedMetric4<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }
        public static ITaggedMetric5<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }
        public static ITaggedMetric6<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }
        public static ITaggedMetric7<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }
        public static ITaggedMetric8<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
    }
}
