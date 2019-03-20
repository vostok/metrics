using System;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    public static partial class MetricContextExtensionsTiming
    {
        public static ITiming Timing(this IMetricContext context, string name, TimingConfig config = null)
        {
            config = config ?? TimingConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Timing(context, tags, config);
        }

        #region TaggedMetric

        public static ITaggedMetricT<TFor, ITiming> Timing<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter, TimingConfig config = null)
        {
            config = config ?? TimingConfig.Default;
            return new TaggedMetricT<TFor, ITiming>(CreateTagsFactory(context, name, config), typeTagsConverter);
        }
         
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
        #endregion

        private static TaggedMetric<Timing> CreateTaggedMetric(IMetricContext context, string name, TimingConfig config = null, params string[] keys)
        {
            config = config ?? TimingConfig.Default;
            return new TaggedMetric<Timing>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Timing> CreateTagsFactory(IMetricContext context, string name, TimingConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Timing(context, finalTags, config);
            };
        }
    }
}
