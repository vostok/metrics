using JetBrains.Annotations;

namespace Vostok.Metrics
{
    internal interface IMetricContextWrapper
    {
        [NotNull]
        IMetricContext BaseContext { get; }
    }
}