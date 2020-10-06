using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Models
{
    [PublicAPI]
    public class AnnotationEvent : IEquatable<AnnotationEvent>
    {
        public AnnotationEvent(DateTimeOffset timestamp, [NotNull] MetricTags tags, [CanBeNull] string description)
        {
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));

            if (tags.Count == 0)
                throw new ArgumentException("Empty tags are not allowed in annotation events.", nameof(tags));

            Timestamp = timestamp;
            Description = description;
        }

        /// <summary>
        /// Timestamp denoting the moment when this <see cref="AnnotationEvent"/>'s was generated.
        /// </summary>
        public DateTimeOffset Timestamp { get; }

        /// <summary>
        /// <para>Set of key-value string tags that identify this event. May not be empty.</para>
        /// <para>See <see cref="MetricTags"/> for more details.</para>
        /// </summary>
        [NotNull]
        public MetricTags Tags { get; }

        /// <summary>
        /// A free-form text description of the described event.
        /// </summary>
        [CanBeNull]
        public string Description { get; }

        #region Equality members

        public bool Equals(AnnotationEvent other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!Tags.Equals(other.Tags))
                return false;

            if (!Timestamp.UtcTicks.Equals(other.Timestamp.UtcTicks))
                return false;

            return string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
            => Equals(obj as AnnotationEvent);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Timestamp.GetHashCode();
                hashCode = (hashCode * 397) ^ Tags.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        } 

        #endregion
    }
}
