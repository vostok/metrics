using JetBrains.Annotations;
using Vostok.Metrics.Model;
using Vostok.Metrics.Primitives.Timer;

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

        /// <summary>
        /// <para><see cref="UpperBound"/> tag represents numerical inclusive upper bound for a histogram bucket.</para>
        /// <para>It has a special value of <c>+Inf</c> to denote positive infinity.</para>
        /// <para>See <see cref="HistogramFactoryExtensions"/> to learn more about histograms.</para>
        /// </summary>
        public const string UpperBound = "_upperBound";
    }
}
