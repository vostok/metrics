using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// <para>Specifies parameters common for all metric primitive in the <see cref="MetricContext"/></para>
    /// <para>This config is "cold": values of the properties should never change.</para>
    /// </summary>
    [PublicAPI]
    public class MetricContextConfig
    {
        public MetricTags Tags { get; set; } = MetricTags.Empty;
        public TimeSpan DefaultScrapePeriod { get; set; } = TimeSpan.FromMinutes(1);
        
        internal static readonly MetricContextConfig Default = new MetricContextConfig();
    }
}