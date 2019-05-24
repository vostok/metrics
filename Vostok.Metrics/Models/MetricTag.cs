using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Models
{
    /// <summary>
    /// Represents a single item in <see cref="MetricTags"/> collection.
    /// </summary>
    [PublicAPI]
    public class MetricTag : IEquatable<MetricTag>
    {
        public MetricTag([NotNull] string key, [NotNull] string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        [NotNull]
        public string Key { get; }

        [NotNull]
        public string Value { get; }

        public override string ToString() =>
            $"{nameof(Key)}: {Key}, {nameof(Value)}: {Value}";

        #region Equality

        public bool Equals(MetricTag other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(Key, other.Key) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
            => Equals(obj as MetricTag);

        public override int GetHashCode()
        {
            unchecked
            {
                return (GetStableHashCode(Key) * 397) ^ GetStableHashCode(Value);
            }

            // (iloktionov): Default implementation of string.GetHashCode() returns values that may vary from machine to machine.
            // (iloktionov): We require a stable hash code implementation in order to facilitate sharding of metric events by tags hash.
            int GetStableHashCode(string str)
            {
                unchecked
                {
                    if (string.IsNullOrEmpty(str))
                        return 0;

                    var length = str.Length;
                    var hash = (uint)length;

                    var remainder = length & 1;

                    length >>= 1;

                    var index = 0;
                    for (; length > 0; length--)
                    {
                        hash += str[index];
                        var temp = (uint)(str[index + 1] << 11) ^ hash;
                        hash = (hash << 16) ^ temp;
                        index += 2;
                        hash += hash >> 11;
                    }

                    if (remainder == 1)
                    {
                        hash += str[index];
                        hash ^= hash << 11;
                        hash += hash >> 17;
                    }

                    hash ^= hash << 3;
                    hash += hash >> 5;
                    hash ^= hash << 4;
                    hash += hash >> 17;
                    hash ^= hash << 25;
                    hash += hash >> 6;

                    return (int) hash;
                }
            }
        }

        #endregion
    }
}
