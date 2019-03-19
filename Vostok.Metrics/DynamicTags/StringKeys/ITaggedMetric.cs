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
    public interface ITaggedMetric4<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4);
    }
    public interface ITaggedMetric5<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5);
    }
    public interface ITaggedMetric6<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6);
    }
    public interface ITaggedMetric7<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7);
    }
    public interface ITaggedMetric8<out TMetric> : IDisposable
    {
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8);
    }
}