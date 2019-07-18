using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public static class ListFuncGaugeFactoryExtensions
    {
        /// <inheritdoc cref="IFuncGauge"/>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="valuesProvider">
        ///     <para>Delegate, that returns objects with value and dynamic tags (optional).</para>
        ///     <para>Mark object property as <see cref="MetricEvent.Value"/> source with <see cref="MetricValueAttribute"/>.</para>
        ///     <para>Mark object properties as dynamic <see cref="MetricEvent.Tags"/> source with <see cref="MetricValueAttribute"/>.</para>
        /// </param>
        /// <param name="config">Optional metric-specific config.</param>
        [NotNull]
        public static IListFuncGauge CreateListFuncGauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] Func<IEnumerable<object>> valuesProvider, [CanBeNull] ListFuncGaugeConfig config = null)
            => new ListFuncGauge(context, name, valuesProvider, config ?? ListFuncGaugeConfig.Default);
    }
}