namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    public class GaugeConfig
    {
        public string Unit { get; set; }
        public string AggregationType { get; set; } = null;
        
        internal static readonly GaugeConfig Default = new GaugeConfig();
    }
}