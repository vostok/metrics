using System;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Timer
{
    internal static class Quantiles
    {
        public static double[] DefaultQuantiles => new[] { 0.5, 0.75, 0.95, 0.99, 0.999 };

        /// <param name="values">Sorted values.</param>
        public static double GetQuantile(double quantile, double[] values, int size)
        {
            if (size == 0)
                return 0;

            var position = quantile * (size + 1);
            var index = (int)Math.Round(position);

            if (index < 0)
                index = 0;
            if (index >= size)
                index = size - 1;

            return values[index];
        }

        public static MetricTags[] QuantileTags(double[] quantiles, MetricTags baseTags)
        {
            var quantileTags = new MetricTags[quantiles.Length];

            for (var i = 0; i < quantiles.Length; i++)
            {
                var quantileValue = quantiles[i];
                if (quantileValue < 0d || quantileValue > 1d)
                    throw new ArgumentOutOfRangeException(nameof(quantiles), $"One of provided quantiles has incorrect value '{quantileValue}'.");

                quantileValue *= 100d;

                while (Math.Abs(Math.Truncate(quantileValue) - quantileValue) > double.Epsilon)
                    quantileValue *= 10d;

                quantileTags[i] = baseTags.Append(WellKnownTagKeys.Aggregate, "p" + (int)quantileValue);
            }

            return quantileTags;
        }
    }
}