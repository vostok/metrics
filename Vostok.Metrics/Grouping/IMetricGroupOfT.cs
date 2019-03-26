using JetBrains.Annotations;

namespace Vostok.Metrics.Grouping
{
    [PublicAPI]
    public interface IMetricGroup<in TFor, out TMetric>
    {
        [NotNull]
        TMetric For([NotNull] TFor value);
    }
}