using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="GaugeDocumentation"/>
    [PublicAPI]
    public interface IFuncGauge : IDisposable
    {
        void SetValueProvider([NotNull] Func<double> valueProvider);
    }
}