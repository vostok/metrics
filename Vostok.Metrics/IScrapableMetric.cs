using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    [PublicAPI]
    public interface IScrapableMetric
    {
        IEnumerable<MetricEvent> Scrape();
    }
}