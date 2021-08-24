using System;
using JetBrains.Annotations;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics
{
    /// <inheritdoc cref="IMetricContext"/>
    [PublicAPI]
    public interface IScrapeConfigurableMetricContext : IMetricContext
    {
        /// <inheritdoc cref="IMetricContext.Register"/>
        /// <param name="scrapeConfig">Scrape configuration</param>
        [NotNull]
        IDisposable Register([NotNull] IScrapableMetric metric, [CanBeNull] TimeSpan? scrapePeriod, [CanBeNull] ScrapeConfig scrapeConfig);
    }
}