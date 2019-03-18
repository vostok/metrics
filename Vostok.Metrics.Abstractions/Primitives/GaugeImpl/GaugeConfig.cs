using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Primitives.GaugeImpl
{
    public class GaugeConfig
    {
        [CanBeNull] public string Unit { get; set; }  
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly GaugeConfig Default = new GaugeConfig();
    }
}