using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics
{
    [PublicAPI]
    public static class IMetricContextExtensions_Sending
    {
        /// <summary>
        /// Converts given <see cref="MetricDataPoint"/> to a <see cref="MetricEvent"/> using given <paramref name="context"/>'s tags and sends it for further processing.
        /// </summary>
        public static void Send([NotNull] this IMetricContext context, [NotNull] MetricDataPoint point)
            => context.Send(point.ToMetricEvent(context.Tags));
    }
}