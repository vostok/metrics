using System;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    public static partial class MetricContextExtensionsGauge
    {
        public static IDisposable Gauge(this IMetricContext context, string name, Func<double> getValue, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new FuncGauge(context, tags, getValue, config);
        }

        public static IGauge Gauge(this IMetricContext context, string name, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Gauge(context, tags, config);
        }

        public static ITaggedMetricT<TFor, IGauge> Gauge<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter = null, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            typeTagsConverter = typeTagsConverter ?? TypeTagsConverter<TFor>.Default;
            return new TaggedMetricT<TFor, IGauge>(
                context,
                CreateGaugeFactory(context, name, config),
                config.ScrapePeriod,
                typeTagsConverter);
        }

        private static TaggedMetric<Gauge> CreateTaggedMetric(IMetricContext context, string name, GaugeConfig config, params string[] keys)
        {
            config = config ?? GaugeConfig.Default;
            return new TaggedMetric<Gauge>(
                context,
                CreateGaugeFactory(context, name, config),
                config.ScrapePeriod,
                keys);
        }

        private static Func<MetricTags, Gauge> CreateGaugeFactory(IMetricContext context, string name, GaugeConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Gauge(context, finalTags, config);
            };
        }
    }
}