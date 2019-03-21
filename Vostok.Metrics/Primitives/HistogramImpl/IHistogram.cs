using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.HistogramImpl
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
    /// Consider using <see cref="Vostok.Metrics.Primitives.TimingImpl.ITiming"/> instead of Histogram.
    /// Timing calculates "fair" percentiles and doesn't require configuration.
    /// However it is not suitable for high workloads.
    /// </para>
    /// <para>
    /// To create a Histogram use <see cref="MetricContextExtensionsHistogram">extensions</see> for <see cref="IMetricContext"/>.
    /// </para>
    /// <para>
    /// Call <see cref="IHistogram.Dispose"/> to stop scraping the metric.
    /// </para>
    /// </remarks>
    //todo example
    [PublicAPI]
    public interface IHistogram : IDisposable
    {
        void Report(double value);
        [CanBeNull] string Unit { get; }
    }
}