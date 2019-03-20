using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.DynamicTags.StringKeys
{
    
    [PublicAPI]
    public interface ITaggedMetric1<out TMetric> : IDisposable
    {
        TMetric For(string value1);
    }
    
    [PublicAPI]
    public interface ITaggedMetric2<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2);
    }
    
    [PublicAPI]
    public interface ITaggedMetric3<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3);
    }
    
    [PublicAPI]
    public interface ITaggedMetric4<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4);
    }
    
    [PublicAPI]
    public interface ITaggedMetric5<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5);
    }
    
    [PublicAPI]
    public interface ITaggedMetric6<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6);
    }
    
    [PublicAPI]
    public interface ITaggedMetric7<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7);
    }
    
    [PublicAPI]
    public interface ITaggedMetric8<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8);
    }
}