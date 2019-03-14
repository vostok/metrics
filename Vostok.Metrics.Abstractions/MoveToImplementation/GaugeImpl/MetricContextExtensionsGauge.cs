using System;
using System.Collections.Generic;
using Vostok.Metrics.Abstractions.DynamicTags;
using Vostok.Metrics.Abstractions.DynamicTags.StringKeys;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    public static class MetricContextExtensionsGauge
    {
        public static void Gauge(this IMetricContext context, string name, Func<double> getValue, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config = null)
        {
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            var gauge = new FuncGauge(getValue, tags, config ?? GaugeConfig.Default);
            registration = context.Register(gauge, scrapePeriod);
        }

        public static IGauge Gauge(this IMetricContext context, string name, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config = null)
        {
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            var gauge = new Gauge(tags, config ?? GaugeConfig.Default);
            registration = context.Register(gauge, scrapePeriod);
            return gauge;
        }

        private static StringKeysTaggedMetric<Gauge> CreateStringTaggedMetric(IMetricContext context, string name, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config, params string[] keys)
        {
            var result = new StringKeysTaggedMetric<Gauge>(
                tags =>
                {
                    var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                    return new Gauge(finalTags, config ?? GaugeConfig.Default);
                },
                keys);
            registration = context.Register(result, scrapePeriod);
            return result;
        }

        //desing User who writes new primitive is forced to boilerplate or use code generation here
        // Should we try to avoid this?
        public static ITaggedMetric1<IGauge> Gauge(this IMetricContext context, string name, string key1, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config = null)
        {
            return CreateStringTaggedMetric(context, name, out registration, scrapePeriod, config, key1);
        }

        public static ITaggedMetric2<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config = null)
        {
            return CreateStringTaggedMetric(context, name, out registration, scrapePeriod, config, key1, key2);
        }
        
        public static ITaggedMetric3<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config = null)
        {
            return CreateStringTaggedMetric(context, name, out registration, scrapePeriod, config, key1, key2, key3);
        }
    }
}