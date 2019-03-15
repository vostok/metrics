using System;

namespace Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl
{
    // todo Get rid of disposable token
    public interface IGauge : IDisposable
    {
        void Set(double value);
        void Add(double value);
        
        string Unit { get; }
    }
}