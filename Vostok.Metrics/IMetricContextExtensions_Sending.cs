using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics
{
    [PublicAPI]
    public static class IMetricContextExtensions_Sending
    {
        public static void Send([NotNull] this IMetricContext context, [NotNull] MetricDataPoint point)
            => context.Send(point.ToMetricEvent(context.Tags));
    }
}