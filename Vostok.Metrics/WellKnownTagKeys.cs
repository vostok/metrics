using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    /// <summary>
    /// Names for some of well-recognized metric tag <see cref="MetricTag.Key">keys</see>.
    /// </summary>
    [PublicAPI]
    public static class WellKnownTagKeys
    {
        /// <summary>
        /// <para><see cref="Name"/> tag represents the physical meaning of the metric being gathered.</para>
        /// <para>It's value usually allows to infer the nature of corresponding measurement unit.</para>
        /// <para>Here are some valid examples of metric names: <c>'requestsPerSecond'</c>, <c>'queueSize'</c>, <c>'writeLatency'</c>.</para>
        /// </summary>
        public const string Name = "_name";
    }
}
