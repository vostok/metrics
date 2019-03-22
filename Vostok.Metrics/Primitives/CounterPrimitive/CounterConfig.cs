using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.CounterPrimitive
{
    [PublicAPI]
    public class CounterConfig
    {
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricUnits")]
        public string Unit { get; set; }
        
        internal static readonly CounterConfig Default = new CounterConfig();
    }
}