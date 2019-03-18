using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    internal class Histogram : IHistogram, IScrapableMetric
    {
        private readonly MetricTags tags;
        private readonly HistogramConfig config;
        private readonly IDisposable registration;
        
        public Histogram([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] HistogramConfig config)
        {
            this.tags = tags;
            this.config = config;
            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricEvent> Scrape()
        {
            throw new NotImplementedException();
        }

        public void Report(double value)
        {
            throw new NotImplementedException();
        }

        public string Unit => config.Unit;

        public void Dispose()
        {
            registration.Dispose();
        }
    }
}