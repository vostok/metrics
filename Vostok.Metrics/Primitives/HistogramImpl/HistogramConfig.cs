using System;
using JetBrains.Annotations;
using Vostok.Metrics.WellKnownConstants;

namespace Vostok.Metrics.Primitives.HistogramImpl
{
    public class HistogramConfig
    {
        public double[] Buckets { get; set; }

        [CanBeNull]
        [ValueProvider("Vostok.Metrics.WellKnownConstants.MetricUnits")]
        public string Unit { get; set; } = MetricUnits.Seconds;
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly HistogramConfig Default = new HistogramConfig();
    }
}