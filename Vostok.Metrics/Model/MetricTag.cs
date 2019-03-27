using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Model
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
                    var hash1 = 5381;
                    var hash2 = hash1;

                    for (var i = 0; i < str.Length && str[i] != '\0'; i += 2)
                    {
                        hash1 = ((hash1 << 5) + hash1) ^ str[i];

                        if (i == str.Length - 1 || str[i + 1] == '\0')
                            break;

                        hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                    }

                    return hash1 + hash2 * 1566083941;
                }
            }
        }

        #endregion
    }
}
