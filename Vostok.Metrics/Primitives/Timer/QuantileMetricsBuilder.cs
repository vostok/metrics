using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Timer
{
    /// <summary>
    /// Builds array of <see cref="MetricEvent"/>'s from array of values and quantiles.
    /// </summary>
    [PublicAPI]
    public class QuantileMetricsBuilder
    {
        private readonly double[] quantiles;
        private readonly string unit;
        private readonly MetricTags countTags;
        private readonly MetricTags minTags;
        private readonly MetricTags maxTags;
        private readonly MetricTags averageTags;
        private readonly MetricTags[] quantileTags;

        public QuantileMetricsBuilder([CanBeNull] double[] quantiles, [NotNull] MetricTags tags, string unit)
        {
            this.quantiles = quantiles;
            this.unit = unit;

            if (quantiles != null)
                quantileTags = Quantiles.QuantileTags(quantiles, tags);

            countTags = tags.Append(WellKnownTagKeys.Aggregate, WellKnownTagValues.AggregateCount);
            minTags = tags.Append(WellKnownTagKeys.Aggregate, WellKnownTagValues.AggregateMin);
            maxTags = tags.Append(WellKnownTagKeys.Aggregate, WellKnownTagValues.AggregateMax);
            averageTags = tags.Append(WellKnownTagKeys.Aggregate, WellKnownTagValues.AggregateAverage);
        }

        public IEnumerable<MetricEvent> Build(double[] values, DateTimeOffset timestamp)
            => Build(values, values.Length, values.Length, timestamp);

        public IEnumerable<MetricEvent> Build(double[] values, int size, int totalCount, DateTimeOffset timestamp)
        {
            Array.Sort(values, 0, size);
            
            var result = new List<MetricEvent>
            {
                new MetricEvent(totalCount, countTags, timestamp, null, null, null),
                new MetricEvent(GetMin(values, size), minTags, timestamp, unit, null, null),
                new MetricEvent(GetMax(values, size), maxTags, timestamp, unit, null, null),
                new MetricEvent(GetAverage(values, size), averageTags, timestamp, unit, null, null)
            };

            if (quantiles == null)
                return result;

            for (var i = 0; i < quantiles.Length; i++)
            {
                result.Add(new MetricEvent(
                    Quantiles.GetQuantile(quantiles[i], values, size), quantileTags[i], timestamp, unit, null, null));
            }

            return result;
        }

        private static double GetAverage(double[] values, int size)
            => size == 0 ? 0 : values.Take(size).Average();

        private static double GetMin(double[] values, int size)
            => size == 0 ? 0 : values.Take(size).Min();

        private static double GetMax(double[] values, int size)
            => size == 0 ? 0 : values.Take(size).Max();
    }
}