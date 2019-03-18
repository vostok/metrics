namespace Vostok.Metrics.Primitives
{
    public interface ISummary
    {
        void Report(double value);
    }
}