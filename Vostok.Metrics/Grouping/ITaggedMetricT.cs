using JetBrains.Annotations;

namespace Vostok.Metrics.Grouping
{
    [PublicAPI]
    public interface ITaggedMetricT<TFor, out TMetric>
    {
        TMetric For(TFor value);
    }
}