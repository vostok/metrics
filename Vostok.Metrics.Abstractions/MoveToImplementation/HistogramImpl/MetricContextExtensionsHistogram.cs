using System;
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

        private static StringKeysTaggedMetric<Histogram> CreateStringKeysTaggedHistogram(IMetricContext context, string name, TimeSpan scrapePeriod, out IDisposable registration, HistogramConfig config = null, params string[] keys)
        {
            config = config ?? HistogramConfig.Default;
            var taggedMetric = new StringKeysTaggedMetric<Histogram>(tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                var metric = new Histogram(config, finalTags);
                return metric;
            }, keys);
            registration = context.Register(taggedMetric, scrapePeriod);

            return taggedMetric;
        }
        
        public static ITaggedMetric1<IHistogram> Histogram(this IMetricContext context, string name, string key1, TimeSpan scrapePeriod, out IDisposable registration, HistogramConfig config = null)
        {
            return CreateStringKeysTaggedHistogram(context, name, scrapePeriod, out registration, config, key1);
        }
        
        public static ITaggedMetric2<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, TimeSpan scrapePeriod, out IDisposable registration, HistogramConfig config = null)
        {
            return CreateStringKeysTaggedHistogram(context, name, scrapePeriod, out registration, config, key1, key2);
        }
        
        public static ITaggedMetric3<IHistogram> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, TimeSpan scrapePeriod, out IDisposable registration, HistogramConfig config = null)
        {
            return CreateStringKeysTaggedHistogram(context, name, scrapePeriod, out registration, config, key1, key2, key3);
        }
    }
}