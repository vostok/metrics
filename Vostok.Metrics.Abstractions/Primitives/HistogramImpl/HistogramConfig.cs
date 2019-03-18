using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Primitives.HistogramImpl
{
    public class HistogramConfig
    {
        public double[] Buckets { get; set; }

        [CanBeNull]
        [ValueProvider("Vostok.Metrics.Abstractions.MetricUnits")]
        public string Unit { get; set; } = MetricUnits.Seconds;
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly HistogramConfig Default = new HistogramConfig();
    }
}