using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public static class FuncGaugeFactoryExtensions_Create
    {
        /// <inheritdoc cref="IFuncGauge"/>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="config">Optional metric-specific config.</param>
        [NotNull]
        public static IFuncGauge CreateFuncGauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] Func<double> valueProvider, [CanBeNull] FuncGaugeConfig config = null)
        {
            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            return new FuncGauge(context, MetricTagsMerger.Merge(context.Tags, name), () => valueProvider(), config ?? FuncGaugeConfig.Default);
        }

        /// <inheritdoc cref="IFuncGauge"/>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="config">Optional metric-specific config.</param>
        [NotNull]
        public static IFuncGauge CreateFuncGauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] Func<double?> valueProvider, [CanBeNull] FuncGaugeConfig config = null)
        {
            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            return new FuncGauge(context, MetricTagsMerger.Merge(context.Tags, name), valueProvider, config ?? FuncGaugeConfig.Default);
        }
    }
}