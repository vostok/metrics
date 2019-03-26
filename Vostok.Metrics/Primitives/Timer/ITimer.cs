using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Timer
{
    /// <summary>
    /// todo tell about different timer implementations here
    /// </summary>
    [PublicAPI]
    public interface ITimer : IDisposable
    {
        void Report(double value);
        string Unit { get; }
    }
}