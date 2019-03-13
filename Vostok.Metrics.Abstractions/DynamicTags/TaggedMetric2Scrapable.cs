using System;
using System.Collections.Generic;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags
{
    // auto-generate all TaggedMetricNScrapable classes
    internal class TaggedMetric2Scrapable<TMetric> : TaggedMetric2<TMetric>, IScrapableMetric
        where TMetric : IScrapableMetric
    {
        public TaggedMetric2Scrapable(Func<MetricTags, TMetric> factory, string key1, string key2)
            : base(factory, key1, key2)
        {
        }
        
        public IEnumerable<MetricEvent> Scrape()
        {
            foreach (var kvp in cache)
            {
                foreach (var metricEvent in kvp.Value.Scrape())
                {
                    yield return metricEvent;
                }
            }
        }
    }
}