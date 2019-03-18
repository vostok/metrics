using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Primitives.TimingImpl
{
    public class TimingConfig
    {
        internal static readonly TimingConfig Default = new TimingConfig();
        
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.Abstractions.MetricUnits")]
        public string Unit { get; set; } = MetricUnits.Seconds;
    }
}