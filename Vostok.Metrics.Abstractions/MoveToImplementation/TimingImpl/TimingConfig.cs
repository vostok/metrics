namespace Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl
{
    public class TimingConfig
    {
        private const string TimingAggregationType = "Timing";
        private const string TimingDefaultUnit = "seconds";
        
        internal static readonly TimingConfig Default = new TimingConfig();

        public string Unit { get; set; } = TimingDefaultUnit;
        public string AggregationType { get; set; } = TimingAggregationType;
    }
}