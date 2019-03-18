namespace Vostok.Metrics.Abstractions.Primitives
{
    public interface ISummary
    {
        void Report(double value);
    }
}