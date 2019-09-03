using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="GaugeDocumentation"/>
    [PublicAPI]
    public static class MultiFuncGaugeFactoryExtensions
    {
        public static IDisposable CreateMultiFuncGauge(
            [NotNull] this IMetricContext context,
            [NotNull] Func<IEnumerable<MetricDataPoint>> pointProvider,
            [CanBeNull] FuncGaugeConfig config = null)
            => new MultiFuncGauge(context, pointProvider, config ?? FuncGaugeConfig.Default);
    }
}