using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsValue(double value)
            => CompareWithValue(value) == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareWithValue(double value)
        {
            if (value > RightBound)
                return 1;

            if (value <= LeftBound)
                return -1;

            return 0;
        }
    }
}
