
using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.DynamicTags.StringKeys
{
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric1<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        TMetric For(string value1);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric2<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric3<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        /// <param name="value3">Value of 3 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2, string value3);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric4<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        /// <param name="value3">Value of 3 tag. Keys were defined at construction time.</param>
        /// <param name="value4">Value of 4 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2, string value3, string value4);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric5<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        /// <param name="value3">Value of 3 tag. Keys were defined at construction time.</param>
        /// <param name="value4">Value of 4 tag. Keys were defined at construction time.</param>
        /// <param name="value5">Value of 5 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2, string value3, string value4, string value5);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric6<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        /// <param name="value3">Value of 3 tag. Keys were defined at construction time.</param>
        /// <param name="value4">Value of 4 tag. Keys were defined at construction time.</param>
        /// <param name="value5">Value of 5 tag. Keys were defined at construction time.</param>
        /// <param name="value6">Value of 6 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric7<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        /// <param name="value3">Value of 3 tag. Keys were defined at construction time.</param>
        /// <param name="value4">Value of 4 tag. Keys were defined at construction time.</param>
        /// <param name="value5">Value of 5 tag. Keys were defined at construction time.</param>
        /// <param name="value6">Value of 6 tag. Keys were defined at construction time.</param>
        /// <param name="value7">Value of 7 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7);
    }
    
    /// <summary>
    /// <para>
    /// Represents a group of metrics of type <typeparamref name="TMetric"/>.
    /// </para>
    /// <para>
    /// Metrics in group share the name but have different dynamic tags specified in <see cref="For"/> method
    /// </para>
    /// </summary>
    [PublicAPI]
    public interface ITaggedMetric8<out TMetric> : IDisposable
    {
        /// <summary>
        /// Retrieves a metric with specific tags from this group
        /// </summary>
        /// <param name="value1">Value of 1 tag. Keys were defined at construction time.</param>
        /// <param name="value2">Value of 2 tag. Keys were defined at construction time.</param>
        /// <param name="value3">Value of 3 tag. Keys were defined at construction time.</param>
        /// <param name="value4">Value of 4 tag. Keys were defined at construction time.</param>
        /// <param name="value5">Value of 5 tag. Keys were defined at construction time.</param>
        /// <param name="value6">Value of 6 tag. Keys were defined at construction time.</param>
        /// <param name="value7">Value of 7 tag. Keys were defined at construction time.</param>
        /// <param name="value8">Value of 8 tag. Keys were defined at construction time.</param>
        TMetric For(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8);
    }
}