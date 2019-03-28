using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public class HistogramBuckets
    {
        /// <param name="upperBounds">See <see cref="UpperBounds"/>.</param>
        public HistogramBuckets([NotNull] params double[] upperBounds)
            : this(upperBounds as IReadOnlyList<double>)
        {
        }

        /// <param name="upperBounds">See <see cref="UpperBounds"/>.</param>
        public HistogramBuckets([NotNull] IReadOnlyList<double> upperBounds)
        {
            if (upperBounds == null)
                throw new ArgumentNullException(nameof(upperBounds));

            if (upperBounds.Count == 0)
                throw new ArgumentException("Provided upper bounds list was empty.");

            for (var i = 1; i < upperBounds.Count; i++)
            {
                var currentBound = upperBounds[i];
                var previousBound = upperBounds[i - 1];
                if (previousBound >= currentBound)
                    throw new ArgumentException("Upper bounds sequence must be increasing.");
            }

            if (double.IsPositiveInfinity(upperBounds[upperBounds.Count - 1]))
            {
                UpperBounds = upperBounds;
            }
            else
            {
                var newBounds = new List<double>(upperBounds.Count + 1);

                newBounds.AddRange(upperBounds);
                newBounds.Add(double.PositiveInfinity);

                UpperBounds = newBounds;
            }
        }

        [NotNull]
        public static HistogramBuckets CreateLinear(double start, double width, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Buckets count must be positive.");

            var upperBounds = new double[count];

            for (var i = 0; i < count; i++)
            {
                upperBounds[i] = start;

                start += width;
            }

            return new HistogramBuckets(upperBounds);
        }

        [NotNull]
        public static HistogramBuckets CreateExponential(double start, double factor, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Buckets count must be positive.");

            if (start <= 0)
                throw new ArgumentOutOfRangeException(nameof(start), start, "Starting value must be positive.");

            if (factor <= 1)
                throw new ArgumentOutOfRangeException(nameof(factor), factor, "Exponential factor must be > 1.");

            var upperBounds = new double[count];

            for (var i = 0; i < count; i++)
            {
                upperBounds[i] = start;

                start *= factor;
            }

            return new HistogramBuckets(upperBounds);
        }

        /// <summary>
        /// <para>A set of inclusive upper bounds of histogram's buckets.</para>
        /// <para>Must contain a monotonically increasing sequence of values.</para>
        /// <para>Must contain <see cref="double.PositiveInfinity"/> as the last element.</para>
        /// <para>Must not be empty.</para>
        /// </summary>
        [NotNull]
        public IReadOnlyList<double> UpperBounds { get; }

        [CanBeNull]
        public static HistogramBuckets operator+([CanBeNull] HistogramBuckets left, [CanBeNull] HistogramBuckets right)
        {
            if (left == null)
                return right;

            if (right == null)
                return left;

            var upperBounds = new List<double>(left.UpperBounds.Count - 1 + right.UpperBounds.Count);

            upperBounds.AddRange(left.UpperBounds.Take(left.UpperBounds.Count - 1));
            upperBounds.AddRange(right.UpperBounds);

            return new HistogramBuckets(upperBounds);
        }
    }
}
