using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Counter
{
    /// <summary>
    /// <para>
    /// Counter represents a value that can only increase.
    /// Counters with the same tags are summed up server side.
    /// </para>
    /// <remarks>
    /// <para>
    /// To create a Counter use <see cref="MetricContextExtensionsCounter">extensions</see> for <see cref="IMetricContext"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// You can use a Counter to represent the number of newly-registered users to your app.
    /// Every replica of your API creates a Counter with name "registered-users".
    /// When API replica registers new user it calls <see cref="ICounterExtensions.Inc"/>.
    /// Values from all replicas are summed up and the single value is saved to a permanent storage.
    /// </para>
    /// </example>
    /// </summary>
    [PublicAPI]
    public interface ICounter
    {
        void Add(double value);
    }
}