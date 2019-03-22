using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.DynamicTags.Typed;

namespace Vostok.Metrics.Model
{
    /// <summary>
    /// <para>
    /// Ordered list of key-value pairs (each pair called a <see cref="MetricTag"/>) distinguishes one metric from another.
    /// Keys and values are both <see cref="string">strings</see>. 
    /// </para>
    /// <para>
    /// Two <see cref="MetricSample">MetricSamples</see> belong to the same metric if and only if their <see cref="MetricSample.Tags"/> are the same.
    /// </para>
    /// <para>
    /// MetricTags collection is immutable and append-only.
    /// </para>
    /// </summary>
    [PublicAPI]
    public class MetricTags : IReadOnlyList<MetricTag>, IEquatable<MetricTags>
    {
        public MetricTags Add([NotNull] MetricTag tag)
        {
            throw new NotImplementedException();
        }

        public MetricTags AddRange([NotNull] MetricTags tags)
        {
            throw new NotImplementedException();
        }
        
        public static readonly MetricTags Empty = new MetricTags();

        #region IReadOnlyList implementation

        public IEnumerator<MetricTag> GetEnumerator() =>
            throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int Count { get; }
        
        public MetricTag this[int index] =>
            throw new NotImplementedException();
        
        public bool Equals(MetricTags other) =>
            throw new NotImplementedException();

        #endregion
    }
}