using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl
{
    /// <summary>
    /// <para>
    /// Histogram allows you to estimate the distribution of values.
    /// </para>
    /// <para>
    /// You configure Histogram by specifying buckets.
    /// Each reported value finds the corresponding bucket and increments the value inside.
    /// Then the buckets are periodically scraped.
    /// </para>
    /// <para>
    /// Histograms are aggregated server-side.
    /// Output of aggregation includes total count, sum and percentiles.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Consider using <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/> instead of Histogram.
    /// Timer calculates "fair" percentiles and doesn't require configuration.
    /// However it is not suitable for high workloads.
    /// </para>
    /// <para>
    /// To create a Histogram use <see cref="MetricContextExtensionsHistogram">extensions</see> for <see cref="IMetricContext"/>.
    /// </para>
    /// <para>
    /// Call <see cref="IDisposable.Dispose"/> to stop scraping the metric.
    /// </para>
    /// </remarks>
    //todo example
    internal class Histogram : ITimer, IScrapableMetric
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

        public IEnumerable<MetricSample> Scrape()
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