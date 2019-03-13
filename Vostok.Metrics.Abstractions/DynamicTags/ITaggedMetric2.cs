namespace Vostok.Metrics.Abstractions.DynamicTags
{
    public interface ITaggedMetric2<out TMetric>
    {
        TMetric For(string value1, string value2);
    }
}