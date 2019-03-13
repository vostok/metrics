namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    public interface IGauge
    {
        void Set(double value);
        
        void Inc();
        void Dec();
        
        void Add(double value);
        void Subtract(double value);
    }
}