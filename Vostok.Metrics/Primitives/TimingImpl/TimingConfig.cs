using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    public class TimingConfig
    {
        internal static readonly TimingConfig Default = new TimingConfig();
        
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.MetricUnits")]
        public string Unit { get; set; } = MetricUnits.Seconds;
    }
}