using JetBrains.Annotations;
using Vostok.Metrics.WellKnownConstants;

namespace Vostok.Metrics.Primitives.TimingImpl
{
    public class TimingConfig
    {
        internal static readonly TimingConfig Default = new TimingConfig();
        
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricUnits")]
        public string Unit { get; set; } = MetricUnits.Seconds;
    }
}