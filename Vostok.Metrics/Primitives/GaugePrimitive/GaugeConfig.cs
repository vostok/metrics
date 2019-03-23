using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.GaugePrimitive
{
    [PublicAPI]
    public class GaugeConfig
    {
        [CanBeNull] 
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; }  
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly GaugeConfig Default = new GaugeConfig();
    }
}