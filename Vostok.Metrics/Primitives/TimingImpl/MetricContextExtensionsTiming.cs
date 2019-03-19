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

        private static Func<MetricTags, Timing> CreateTimingFactory(IMetricContext context, string name, TimingConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Timing(context, finalTags, config);
            };
        }

        private static TaggedMetric<Timing> CreateTaggedMetric(IMetricContext context, string name, TimingConfig config, params string[] keys)
        {
            config = config ?? TimingConfig.Default;
            return new TaggedMetric<Timing>(
                context,
                CreateTimingFactory(context, name, config),
                keys);
        }

        public static ITaggedMetricT<TFor, ITiming> Timing<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter, TimingConfig config = null)
        {
            config = config ?? TimingConfig.Default;
            typeTagsConverter = typeTagsConverter ?? TypeTagsConverter<TFor>.Default;
            return new TaggedMetricT<TFor, ITiming>(
                context,
                CreateTimingFactory(context, name, config),
                typeTagsConverter);
        }
    }
}