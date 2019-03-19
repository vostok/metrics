using Vostok.Metrics.DynamicTags.StringKeys;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    public static partial class MetricContextExtensionsTiming
    {
        public static ITaggedMetric1<ITiming> Timing(this IMetricContext context, string name, string key1, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }
        public static ITaggedMetric2<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }
        public static ITaggedMetric3<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }
        public static ITaggedMetric4<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, string key4, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }
        public static ITaggedMetric5<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }
        public static ITaggedMetric6<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }
        public static ITaggedMetric7<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }
        public static ITaggedMetric8<ITiming> Timing(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, TimingConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
    }
}
