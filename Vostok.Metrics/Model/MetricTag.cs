using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    /// <summary>
    /// Represents a single item in <see cref="MetricTags"/> collection.
    /// </summary>
    [PublicAPI]
    public class MetricTag
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
    }
}
