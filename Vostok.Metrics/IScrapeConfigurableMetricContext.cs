using System;
using JetBrains.Annotations;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics
{
    /// <inheritdoc cref="IMetricContext"/>
    [PublicAPI]
    public interface IScrapeConfigurableMetricContext
    {
        /// <inheritdoc cref="IMetricContext.Register"/>
        /// <param name="scrapableMetricConfig">Scrape configuration</param>
        [NotNull]
        IDisposable Register([NotNull] IScrapableMetric metric, [CanBeNull] ScrapableMetricConfig scrapableMetricConfig);
    }
}