using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.CounterImpl
{
    [PublicAPI]
    public class CounterConfig
    {
        [CanBeNull]
        public string Unit { get; set; }
        
        internal static readonly CounterConfig Default = new CounterConfig();
    }
}