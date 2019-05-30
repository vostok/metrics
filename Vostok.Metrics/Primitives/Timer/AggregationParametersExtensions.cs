using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public static class AggregationParametersExtensions
    {
        private const string AggregatePeriodKey = "_period";
        private const string AggregateLagKey = "_lag";
        private const string QuantilesKey = "_quantiles";
        private const string QuantilesDelimiter = ";";

        [NotNull]
        public static Dictionary<string, string> SetQuantiles([NotNull] this Dictionary<string, string> aggregationParameters, [NotNull] double[] quantiles)
        {
            aggregationParameters = aggregationParameters ?? throw new ArgumentNullException(nameof(aggregationParameters));
            quantiles = quantiles ?? throw new ArgumentNullException(nameof(quantiles));

            aggregationParameters[QuantilesKey] = string.Join(QuantilesDelimiter, quantiles.Select(DoubleSerializer.Serialize));

            return aggregationParameters;
        }

        [CanBeNull]
        public static double[] GetQuantiles([CanBeNull] this IReadOnlyDictionary<string, string> aggregationParameters)
        {
            if (aggregationParameters == null)
                return null;
            if (!aggregationParameters.TryGetValue(QuantilesKey, out var quantiles) || quantiles == null)
                return null;

            return quantiles.Split(new[] {QuantilesDelimiter}, StringSplitOptions.RemoveEmptyEntries).Select(DoubleSerializer.Deserialize).ToArray();
        }

        [NotNull]
        public static Dictionary<string, string> SetAggregatePeriod([NotNull] this Dictionary<string, string> aggregationParameters, TimeSpan period) =>
            SetTimeSpan(aggregationParameters, AggregatePeriodKey, period);

        [CanBeNull]
        public static TimeSpan? GetAggregatePeriod([CanBeNull] this IReadOnlyDictionary<string, string> aggregationParameters) =>
            GetTimeSpan(aggregationParameters, AggregatePeriodKey);

        [NotNull]
        public static Dictionary<string, string> SetAggregateLag([NotNull] this Dictionary<string, string> aggregationParameters, TimeSpan period) =>
            SetTimeSpan(aggregationParameters, AggregateLagKey, period);

        [CanBeNull]
        public static TimeSpan? GetAggregateLag([CanBeNull] this IReadOnlyDictionary<string, string> aggregationParameters) =>
            GetTimeSpan(aggregationParameters, AggregateLagKey);

        [NotNull]
        private static Dictionary<string, string> SetTimeSpan([NotNull] this Dictionary<string, string> aggregationParameters, [NotNull] string key, TimeSpan timeSpan)
        {
            aggregationParameters = aggregationParameters ?? throw new ArgumentNullException(nameof(aggregationParameters));
            key = key ?? throw new ArgumentNullException(nameof(key));

            aggregationParameters[key] = TimeSpanSerializer.Serialize(timeSpan);

            return aggregationParameters;
        }

        [CanBeNull]
        private static TimeSpan? GetTimeSpan([CanBeNull] this IReadOnlyDictionary<string, string> aggregationParameters, [NotNull] string key)
        {
            if (aggregationParameters == null)
                return null;
            if (!aggregationParameters.TryGetValue(key, out var value) || value == null)
                return null;

            return TimeSpanSerializer.Deserialize(value);
        }
    }
}