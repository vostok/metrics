using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public struct HistogramBucket
    {
        public readonly double LowerBound;
        public readonly double UpperBound;

        public HistogramBucket(double lowerBound, double upperBound)
        {
            if (lowerBound >= upperBound)
                throw new ArgumentException($"Incorrect bucket bounds: lower bound {lowerBound} >= upper bound {upperBound}.");

            LowerBound = lowerBound;
            UpperBound = upperBound;
        }
    }
}
