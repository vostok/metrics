using Vostok.Metrics.DynamicTags.StringKeys;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    public static partial class MetricContextExtensionsGauge
    {
        public static ITaggedMetric1<IGauge> Gauge(this IMetricContext context, string name, string key1, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }
        public static ITaggedMetric2<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }
        public static ITaggedMetric3<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }
        public static ITaggedMetric4<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }
        public static ITaggedMetric5<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }
        public static ITaggedMetric6<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }
        public static ITaggedMetric7<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }
        public static ITaggedMetric8<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
    }
}
