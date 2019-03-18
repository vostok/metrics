using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Abstractions.Primitives.HistogramImpl
{
    public class HistogramConfig
    {
        public double[] Buckets { get; set; }
        
        [CanBeNull] public string Unit { get; set; }
        [CanBeNull] public TimeSpan? ScrapePeriod { get; set; }
        
        internal static readonly HistogramConfig Default = new HistogramConfig();
    }
}