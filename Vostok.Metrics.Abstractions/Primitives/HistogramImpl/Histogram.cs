using System;
using System.Collections.Generic;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.Primitives.HistogramImpl
{
    internal class Histogram : IHistogram, IScrapableMetric
    {
        private readonly MetricTags tags;
        private readonly HistogramConfig config;
        private readonly IDisposable registration;
        
        public Histogram(IMetricContext context, MetricTags tags, HistogramConfig config)
        {
            this.tags = tags;
            this.config = config;
            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricEvent> Scrape()
        { 
            yield break;
        }

        public void Report(double value)
        {
        }

        public void Dispose()
        {
            registration.Dispose();
        }
    }
}