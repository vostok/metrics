namespace Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl
{
    public class HistogramConfig
    {
        public double[] Buckets { get; set; }

        public string AggregationType { get; set; } = "Histogram";
        public string Unit { get; set; }
        
        internal static readonly HistogramConfig Default = new HistogramConfig();
    }
}