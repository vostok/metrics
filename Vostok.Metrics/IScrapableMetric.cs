using System.Collections.Generic;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    public interface IScrapableMetric
    {
        IEnumerable<MetricEvent> Scrape();
    }
}