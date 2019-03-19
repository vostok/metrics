using System;
using System.Collections;
using System.Collections.Generic;
using Vostok.Metrics.DynamicTags.Typed;

namespace Vostok.Metrics.Model
{
    public class MetricTags : IReadOnlyList<MetricTag>, IEquatable<MetricTags>
    {
        public MetricTags Add(MetricTag tag)
        {
            throw new NotImplementedException();
        }

        public MetricTags AddRange(MetricTags tags)
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