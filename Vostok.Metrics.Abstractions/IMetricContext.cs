using System;
using Vostok.Metrics.Abstractions.Model;

namespace Vostok.Metrics.Abstractions
{
    /// <summary>
    /// <para><see cref="IMetricContext"/> is an entry-point for Vostok.Metrics library.</para>
    /// <para>It provides all that is necessary to create a metric primitive like <see cref="IGauge"/>, <see cref="ITiming"/>, <see cref="IHistogram"/> and others.</para>
    /// </summary>
    public interface IMetricContext
    {
        /// <summary>
        /// <para>A collection of <see cref="MetricTag"/> associated with this <see cref="IMetricContext"/></para>
        /// <para>All metrics created from this context will share the <see cref="Tags"/></para>
        /// </summary>
        MetricTags Tags { get; }
        
        /// <summary>
        /// <para>Registers <see cref="IScrapableMetric"/> for scraping.</para>
        /// <para>
        /// Once per <paramref name="scrapePeriod"/> the <see cref="IScrapableMetric.Scrape"/> method
        /// will be called on the specified <paramref name="metric"/>.
        /// </para>
        /// </summary>
        /// <param name="metric">The metric to scrape</param>
        /// <param name="scrapePeriod">How often to scrape</param>
        /// <returns>The <see cref="IDisposable"/> token. Call <see cref="IDisposable.Dispose"/> to stop scraping the <paramref name="metric"/></returns>
        IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod);
        
        /// <summary>
        /// <para>Sends the <see cref="MetricEvent"/> for further processing.</para>
        /// </summary>
        void Send(MetricEvent @event);
    }
}