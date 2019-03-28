using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <inheritdoc cref="GaugeDocumentation"/>
    [PublicAPI]
    public interface IIntegerGauge : IDisposable
    {
        /// <summary>
        /// Sets current gauge's value to given <paramref name="value"/>.
        /// </summary>
        void Set(long value);

        /// <summary>
        /// Adds given <paramref name="value"/> to current gauge value.
        /// </summary>
        void Add(long value);

        /// <summary>
        /// Substracts given <paramref name="value"/> from current gauge value.
        /// </summary>
        void Substract(long value);

        /// <summary>
        /// Increments current gauge value by one.
        /// </summary>
        void Increment();

        /// <summary>
        /// Decrements current gauge value by one.
        /// </summary>
        void Decrement();
    }
}