using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.CounterImpl
{
    [PublicAPI]
    public class ICounterExtensions
    {
        void Inc(ICounter counter)
        {
            counter.Add(1);
        }
    }
}