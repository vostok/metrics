using System.Collections.Generic;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl
{
    internal class Histogram : IHistogram, IScrapableMetric
    {
        // tags already contain _name tag and all "dynamic" tags
        public Histogram(HistogramConfig config, MetricTags contextTags)
        {
        }

        public IEnumerable<MetricEvent> Scrape()
        { 
            yield break;
        }

        public void Report(double value)
        {
        }
    }
}