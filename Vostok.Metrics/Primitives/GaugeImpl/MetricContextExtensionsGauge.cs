using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    public static class MetricContextExtensionsGauge
    {
        public static IDisposable Gauge(this IMetricContext context, string name, Func<double> getValue, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new FuncGauge(context, tags, getValue, config);
        }
    }
}