using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.GaugePrimitive
{
    [PublicAPI]
    public static class MetricContextExtensionsFuncGauge
    {
        /// <inheritdoc cref="IGauge"/>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="WellKnownTagKeys.Name"/></param>
        /// <param name="getValue">This func will be called when the value of Gauge is observed</param>
        /// <param name="config">Optional config</param>
        //todo not null annotations
        public static IDisposable Gauge(this IMetricContext context, string name, Func<double> getValue, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new FuncGauge(context, tags, getValue, config);
        }
    }
}