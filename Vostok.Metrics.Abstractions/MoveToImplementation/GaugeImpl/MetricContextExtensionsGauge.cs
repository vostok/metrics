using System;
using Vostok.Metrics.Abstractions.DynamicTags.StringKeys;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    public static class MetricContextExtensionsGauge
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

        private static StringKeysTaggedMetric<Gauge> CreateStringTaggedMetric(IMetricContext context, string name, GaugeConfig config, params string[] keys)
        {
            config = config ?? GaugeConfig.Default;
            return new StringKeysTaggedMetric<Gauge>(
                context,
                tags =>
                {
                    var finalTags1 = MetricTagsMerger.Merge(context.Tags, name, tags);
                    return new Gauge(context, finalTags1, config);
                },
                config.ScrapePeriod,
                keys);
        }

        public static ITaggedMetric1<IGauge> Gauge(this IMetricContext context, string name, string key1, GaugeConfig config = null)
        {
            return CreateStringTaggedMetric(context, name, config, key1);
        }

        public static ITaggedMetric2<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, GaugeConfig config = null)
        {
            return CreateStringTaggedMetric(context, name, config, key1, key2);
        }
        
        public static ITaggedMetric3<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, GaugeConfig config = null)
        {
            return CreateStringTaggedMetric(context, name, config, key1, key2, key3);
        }
    }
}