using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Timer
{
    [PublicAPI]
    public struct HistogramBucket
    {
        public readonly double LeftBound;
        public readonly double RightBound;

        public HistogramBucket(double leftBound, double rightBound)
        {
            LeftBound = leftBound;
            RightBound = rightBound;
        }
    }
}
