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
                return (Key.GetHashCode() * 397) ^ Value.GetHashCode();
            }
        }

        #endregion
    }
}
