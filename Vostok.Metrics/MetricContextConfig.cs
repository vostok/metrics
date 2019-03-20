using System;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    public class MetricContextConfig
    {
        public MetricTags Tags { get; set; } = MetricTags.Empty;
        
        public TimeSpan DefaultScrapePeriod { get; set; } = TimeSpan.FromMinutes(1);
        
        internal static readonly MetricContextConfig Default = new MetricContextConfig();
    }
}