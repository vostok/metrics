using System;
using JetBrains.Annotations;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.CounterImpl
{
    [PublicAPI]
    public static class MetricContextExtensionsCounter
    {
        public static ICounter Counter(this IMetricContext context, string name, CounterConfig config = null)
        {
            config = config ?? CounterConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Counter(context, tags, config);
        }

        #region TaggedMetric

        public static ITaggedMetricT<TFor, ICounter> Counter<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter, CounterConfig config = null)
        {
            config = config ?? CounterConfig.Default;
            return new TaggedMetricT<TFor, ICounter>(CreateTagsFactory(context, name, config), typeTagsConverter);
        }
         
        public static ITaggedMetric1<ICounter> Counter(this IMetricContext context, string name, string key1, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }
        public static ITaggedMetric2<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }
        public static ITaggedMetric3<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }
        public static ITaggedMetric4<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }
        public static ITaggedMetric5<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }
        public static ITaggedMetric6<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }
        public static ITaggedMetric7<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }
        public static ITaggedMetric8<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
        #endregion

        private static TaggedMetric<Counter> CreateTaggedMetric(IMetricContext context, string name, CounterConfig config = null, params string[] keys)
        {
            config = config ?? CounterConfig.Default;
            return new TaggedMetric<Counter>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Counter> CreateTagsFactory(IMetricContext context, string name, CounterConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Counter(context, finalTags, config);
            };
        }
    }
}
