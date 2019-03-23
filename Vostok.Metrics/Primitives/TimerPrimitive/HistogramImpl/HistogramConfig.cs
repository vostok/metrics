using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl
{
    [PublicAPI]
    public class HistogramConfig
    {
        public double[] Buckets { get; set; }

        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownUnits")]
        public string Unit { get; set; } = WellKnownUnits.Time.Seconds;
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly HistogramConfig Default = new HistogramConfig();
    }
}