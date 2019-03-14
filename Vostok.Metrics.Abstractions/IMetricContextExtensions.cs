using System.Runtime.CompilerServices;
using Vostok.Metrics.Abstractions.Model;
using Vostok.Metrics.Abstractions.Wrappers;

namespace Vostok.Metrics.Abstractions
{
    public static class IMetricContextExtensions
    {
        public static IMetricContext WithTag(this IMetricContext context, string key, string value)
        {
            return new MetricContextTagWrapper(context, new MetricTags()); // add key-value
        }
    }
}