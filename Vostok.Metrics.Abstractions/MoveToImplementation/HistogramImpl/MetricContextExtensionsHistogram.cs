using System;
using Vostok.Metrics.Abstractions.DynamicTags;
using Vostok.Metrics.Abstractions.DynamicTags.StringKeys;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl
{
    public static class MetricContextExtensionsHistogram
    {
        public static IHistogram Histogram(this IMetricContext context, string name, TimeSpan scrapePeriod, out IDisposable registration, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            var metric = new Histogram(config, tags);
            registration = context.Register(metric, scrapePeriod);
            return metric;
        }

        public static ITaggedMetric2<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, TimeSpan scrapePeriod, out IDisposable registration, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            var taggedMetric = new TaggedMetric2<Histogram>(tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                var metric = new Histogram(config, finalTags);
                return metric;
            }, key1, key2);
            registration = context.Register(taggedMetric, scrapePeriod);
            
            return taggedMetric;
        }
    }
}