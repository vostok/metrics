using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Primitives.GaugeImpl
{
    public class GaugeConfig
    {
        [CanBeNull] 
        [ValueProvider("Vostok.Metrics.Abstractions.MetricUnits")]
        public string Unit { get; set; }  
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly GaugeConfig Default = new GaugeConfig();
    }
}