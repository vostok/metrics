using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Counter
{
    [PublicAPI]
    public class CounterConfig
    {
        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; }
        
        internal static readonly CounterConfig Default = new CounterConfig();
    }
}