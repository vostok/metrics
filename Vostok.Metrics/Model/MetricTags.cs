using System;
using System.Collections;
using System.Collections.Generic;

namespace Vostok.Metrics.Model
{
    public class MetricTags : IReadOnlyList<MetricTag>, IEquatable<MetricTags>
    {
        public IEnumerator<MetricTag> GetEnumerator() =>
            throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int Count { get; }
        
        public MetricTag this[int index] =>
            throw new NotImplementedException();
        
        public bool Equals(MetricTags other) =>
            throw new NotImplementedException();
    }
}