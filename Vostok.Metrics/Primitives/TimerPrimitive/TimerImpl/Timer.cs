using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl
{
    /// <summary>
    /// <para>
    /// Timer immediately sends reported values to aggregation backend.
    /// Backend produces several metrics including percentiles, sum and count.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// To create a Timer use <see cref="MetricContextExtensionsTimer">extensions</see> for <see cref="IMetricContext"/>
    /// </para>
    /// <para>
    /// Timer could be expensive for high workloads (>10k reports per second).
    /// In this case consider using <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// You can calculate the latency of business operation.
    /// <code>
    /// var timer = context.Timer("my-operation-latency");
    /// using (timer.Measure())
    /// {
    ///    //... business code here
    /// }
    /// </code>
    /// </example>
    internal class Timer : ITimer
    {
        private readonly IMetricContext context;
        private readonly MetricTags tags;
        private readonly TimerConfig config;

        public Timer([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] TimerConfig config)
        {
            this.context = context;
            this.tags = tags;
            this.config = config;
        }

        public void Report(double value)
        {
            var MetricSample = new MetricEvent(
                value,
                tags,
                DateTimeOffset.Now,
                config.Unit,
                WellKnownAggregationTypes.Timer,
                null);
            context.Send(MetricSample);
        }

        public string Unit => config.Unit;

        public void Dispose() {}
    }
}