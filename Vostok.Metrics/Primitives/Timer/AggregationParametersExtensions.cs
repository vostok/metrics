using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public static class AggregationParametersExtensions
    {
        private const string QuantilesKey = "_quantiles";
        private const string QuantilesDelimiter = ";";

        [NotNull]
        public static Dictionary<string, string> AddQuantiles([NotNull] this Dictionary<string, string> aggregationParameters, [NotNull] double[] quantiles)
        {
            aggregationParameters = aggregationParameters ?? throw new ArgumentNullException(nameof(aggregationParameters));
            quantiles = quantiles ?? throw new ArgumentNullException(nameof(quantiles));

            aggregationParameters[QuantilesKey] = string.Join(QuantilesDelimiter, quantiles.Select(DoubleSerializer.Serialize));
            return aggregationParameters;
        }

        [CanBeNull]
        public static double[] GetQuantiles([CanBeNull] this Dictionary<string, string> aggregationParameters)
        {
            if (aggregationParameters == null)
                return null;
            if (!aggregationParameters.TryGetValue(QuantilesKey, out var quantiles) || quantiles == null)
                return null;

            return quantiles.Split(new[] {QuantilesDelimiter}, StringSplitOptions.RemoveEmptyEntries).Select(DoubleSerializer.Deserialize).ToArray();
        }
    }
}