using JetBrains.Annotations;
using Vostok.Metrics.WellKnownConstants;

namespace Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl
{
    [PublicAPI]
    public class TimerConfig
    {
        internal static readonly TimerConfig Default = new TimerConfig();
        
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricUnits")]
        public string Unit { get; set; } = MetricUnits.Seconds;
    }
}