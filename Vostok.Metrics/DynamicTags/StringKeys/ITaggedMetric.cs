using System;

namespace Vostok.Metrics.DynamicTags.StringKeys
{
    public interface ITaggedMetric1<out TMetric> : IDisposable
    {
        TMetric For(string value1);
    }
    
    public interface ITaggedMetric2<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2);
    }

    public interface ITaggedMetric3<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3);
    }
}