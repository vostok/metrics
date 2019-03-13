using System.Collections.Generic;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions
{
    public interface IScrapableMetric
    {
        IEnumerable<MetricEvent> Scrape();
    }
}