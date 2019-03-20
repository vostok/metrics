using System;
using JetBrains.Annotations;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    [PublicAPI]
    public static partial class MetricContextExtensionsGauge
    {
        public static IGauge Gauge(this IMetricContext context, string name, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Gauge(context, tags, config);
        }

        #region TaggedMetric

        public static ITaggedMetricT<TFor, IGauge> Gauge<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            return new TaggedMetricT<TFor, IGauge>(CreateTagsFactory(context, name, config), typeTagsConverter);
        }
         
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
        #endregion

        private static TaggedMetric<Gauge> CreateTaggedMetric(IMetricContext context, string name, GaugeConfig config = null, params string[] keys)
        {
            config = config ?? GaugeConfig.Default;
            return new TaggedMetric<Gauge>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Gauge> CreateTagsFactory(IMetricContext context, string name, GaugeConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Gauge(context, finalTags, config);
            };
        }
    }
}
