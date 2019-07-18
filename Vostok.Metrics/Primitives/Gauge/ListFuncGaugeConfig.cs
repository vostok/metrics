using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public class ListFuncGaugeConfig : GaugeConfig
    {
        internal static readonly ListFuncGaugeConfig Default = new ListFuncGaugeConfig();
    }
}