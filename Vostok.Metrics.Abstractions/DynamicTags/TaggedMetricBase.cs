using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.DynamicTags
{
    //design this class and its inheritors are public because they allow user to create custom Metric types
    public class TaggedMetricBase<TMetric> : IScrapableMetric
    {
        private readonly ConcurrentDictionary<MetricTags, TMetric> cache = new ConcurrentDictionary<MetricTags, TMetric>();
        private readonly Func<MetricTags, TMetric> factory;

        public TaggedMetricBase(Func<MetricTags, TMetric> factory)
        {
            this.factory = factory;
        }

        public TMetric For(MetricTags dynamicTags)
        {
            return cache.GetOrAdd(dynamicTags, factory);
        }
        
        public IEnumerable<MetricEvent> Scrape()
        {
            foreach (var kvp in cache)
            {
                //design do smth with this runtime check
                if (kvp.Value is IScrapableMetric scrapable)
                {
                    foreach (var metricEvent in scrapable.Scrape())
                    {
                        yield return metricEvent;
                    }
                }
            }
        }
    }
}