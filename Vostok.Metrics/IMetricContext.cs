using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;
using Vostok.Metrics.Primitives.GaugeImpl;
using Vostok.Metrics.Primitives.HistogramImpl;
using Vostok.Metrics.Primitives.TimingImpl;

namespace Vostok.Metrics
{
    /// <summary>
    /// <para><see cref="IMetricContext"/> is an entry-point for Vostok.Metrics library.</para>
    /// <para>It provides all that is necessary to create a metric primitive like <see cref="IGauge"/>, <see cref="ITiming"/>, <see cref="IHistogram"/> and others.</para>
    /// <para>To create <see cref="IMetricContext"/> instance use <see cref="MetricContext"/>.</para>
    /// </summary>
    [PublicAPI]
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
        /// <para>
        /// Implementations should guarantee that <see cref="IScrapableMetric.Scrape"/> is never called concurrently on the same <paramref name="metric"/>.
        /// </para>
        /// </summary>
        /// <param name="metric">The metric to scrape</param>
        /// <param name="scrapePeriod">How often to scrape</param>
        /// <returns>The <see cref="IDisposable"/> token. Call <see cref="IDisposable.Dispose"/> to stop scraping the <paramref name="metric"/></returns>
        IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod);
        
        /// <summary>
        /// <para>Sends the <see cref="MetricSample"/> for further processing.</para>
        /// <para>
        /// Use this method directly to send custom MetricSample.
        /// To create <see cref="MetricSample"/> you may use <see cref="MetricSampleBuilder"/>.
        /// </para>
        /// </summary>
        void Send(MetricSample sample);
    }
}