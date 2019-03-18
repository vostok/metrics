using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.DynamicTags
{
    //design this class and its inheritors are public because they allow user to create custom Metric types
    public class TaggedMetricBase<TMetric> : IScrapableMetric, IDisposable
    {
        private static readonly bool isTMetricScrapable = typeof(IScrapableMetric).IsAssignableFrom(typeof(TMetric));
        
        private readonly ConcurrentDictionary<MetricTags, TMetric> cache = new ConcurrentDictionary<MetricTags, TMetric>();
        private readonly Func<MetricTags, TMetric> factory;
        private readonly IDisposable registration;

        public TaggedMetricBase(IMetricContext context, Func<MetricTags, TMetric> factory)
        {
            this.factory = factory;
        }

        public TaggedMetricBase(IMetricContext context, Func<MetricTags, TMetric> factory, TimeSpan? scrapePeriod)
        {
            if (!isTMetricScrapable)
            {
                throw new ArgumentException($"{nameof(TMetric)} should implement {nameof(IScrapableMetric)} to use this constructor");
            }
            
            this.factory = factory;
            registration = context.Register(this, scrapePeriod);
        }

        public TMetric For(MetricTags dynamicTags)
        {
            return cache.GetOrAdd(dynamicTags, factory);
        }
        
        public IEnumerable<MetricEvent> Scrape()
        {
            if (!isTMetricScrapable)
            {
                yield break;
            }
            
            foreach (var kvp in cache)
            {
                var scrapable = (IScrapableMetric) kvp.Value;
                foreach (var metricEvent in scrapable.Scrape())
                {
                    yield return metricEvent;
                }
            }
        }

        public void Dispose()
        {
            registration?.Dispose();
        }
    }
}