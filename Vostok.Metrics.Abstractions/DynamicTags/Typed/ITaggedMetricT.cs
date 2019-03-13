namespace Vostok.Metrics.Abstractions.DynamicTags.Typed
{
    public interface ITaggedMetricT<out TMetric, TFor>
    {
        TMetric For(TFor value);
    }
}