namespace Vostok.Metrics.Abstractions.DynamicTags
{
    public interface ITaggedMetric<out TMetric, TFor>
    {
        TMetric For(TFor value);
    }
}