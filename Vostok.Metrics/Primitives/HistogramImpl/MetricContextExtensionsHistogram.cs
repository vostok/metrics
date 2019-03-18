using System;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    public static class MetricContextExtensionsHistogram
    {
        public static IHistogram Histogram(this IMetricContext context, string name, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Histogram(context, tags, config);
        }

        private static Func<MetricTags, Histogram> CreateTagsFactory(IMetricContext context, string name, HistogramConfig config)
        {
            return tags =>
            {
                var finalTags1 = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Histogram(context, finalTags1, config);
            };
        }
        
        private static StringKeysTaggedMetric<Histogram> CreateStringKeysTaggedHistogram(IMetricContext context, string name, HistogramConfig config = null, params string[] keys)
        {
            config = config ?? HistogramConfig.Default;
            return new StringKeysTaggedMetric<Histogram>(
                context, 
                CreateTagsFactory(context, name, config),
                config.ScrapePeriod,
                keys);
        }

        public static ITaggedMetricT<TFor, IHistogram> Histogram<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            typeTagsConverter = typeTagsConverter ?? TypeTagsConverter<TFor>.Default;
            return new TaggedMetricT<TFor, IHistogram>(
                context,
                CreateTagsFactory(context, name, config),
                config.ScrapePeriod,
                typeTagsConverter);
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