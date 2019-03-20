using JetBrains.Annotations;

namespace Vostok.Metrics.DynamicTags.Typed
{
    [PublicAPI]
    public interface ITaggedMetricT<TFor, out TMetric>
    {
        TMetric For(TFor value);
    }
}