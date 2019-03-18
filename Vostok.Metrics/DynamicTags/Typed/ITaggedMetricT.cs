namespace Vostok.Metrics.DynamicTags.Typed
{
    public interface ITaggedMetricT<TFor, out TMetric>
    {
        TMetric For(TFor value);
    }
}