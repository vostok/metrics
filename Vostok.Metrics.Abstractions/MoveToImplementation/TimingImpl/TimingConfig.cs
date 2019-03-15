namespace Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl
{
    public class TimingConfig
    {
        private const string TimingAggregationType = "Timing";
        private const string TimingDefaultUnit = "seconds";
        
        internal static readonly TimingConfig Default = new TimingConfig();

        public string Unit { get; set; } = TimingDefaultUnit;
        //CR(ezsilmar) Remove this from config
        public string AggregationType { get; set; } = TimingAggregationType;
    }
}