namespace Vostok.Metrics.Abstractions.DynamicTags.StringKeys
{
    public interface ITaggedMetric2<out TMetric>
    {
        TMetric For(string value1, string value2);
    }
}