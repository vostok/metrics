using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    /// <summary>
    /// <para>
    /// Timing immediately sends reported values to aggregation backend.
    /// Backend produces several metrics including percentiles, sum and count.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// To create a Timing use <see cref="MetricContextExtensionsTiming">extensions</see> for <see cref="IMetricContext"/>
    /// </para>
    /// <para>
    /// Timing could be expensive for high workloads (>10k reports per second).
    /// In this case consider using <see cref="Vostok.Metrics.Primitives.HistogramImpl.IHistogram"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// You can calculate the latency of business operation.
    /// <code>
    /// var timing = context.Timing("my-operation-latency");
    /// using (timing.Measure())
    /// {
    ///    //... business code here
    /// }
    /// </code>
    /// </example>
    [PublicAPI]
    public interface ITiming
    {
        void Report(double value);
        [CanBeNull] string Unit { get; }
    }
}