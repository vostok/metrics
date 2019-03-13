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

        public static ITaggedMetric2<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, out IDisposable registration, TimeSpan scrapePeriod, GaugeConfig config = null)
        {
            var result = new TaggedMetric2<Gauge>(
                tags =>
                {
                    var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                    return new Gauge(finalTags, config);
                },
                key1,
                key2);
            registration = context.Register(result, scrapePeriod);
            return result;
        }
    }
}