using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    //todo libs
    // 1. HerculesSender
    // 2. ILogSender
    // 3. Abstractions:
    //     - AdHocSender
    //     - 
    // 4. Aggregations lib
    // 5. WinSysMetrics, LinuxSysMetrics
    
    public static class IMetricContextExtensions
    {
        public static IMetricContext WithTag(this IMetricContext context, string key, string value)
        {
            return new MetricContextTagWrapper(context, new MetricTags()); // add key-value
        }
    }
}