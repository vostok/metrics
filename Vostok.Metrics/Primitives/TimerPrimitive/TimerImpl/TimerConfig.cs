using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl
{
    [PublicAPI]
    public class TimerConfig
    {
        internal static readonly TimerConfig Default = new TimerConfig();
        
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; } = WellKnownUnits.Time.Seconds;
    }
}