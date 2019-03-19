using System;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    public static partial class MetricContextExtensionsHistogram
    {
        public static IHistogram Histogram(this IMetricContext context, string name, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Histogram(context, tags, config);
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

        private static TaggedMetric<Histogram> CreateTaggedMetric(IMetricContext context, string name, HistogramConfig config = null, params string[] keys)
        {
            config = config ?? HistogramConfig.Default;
            return new TaggedMetric<Histogram>(
                context, 
                CreateTagsFactory(context, name, config),
                config.ScrapePeriod,
                keys);
        }

        private static Func<MetricTags, Histogram> CreateTagsFactory(IMetricContext context, string name, HistogramConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Histogram(context, finalTags, config);
            };
        }
    }
}