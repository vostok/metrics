using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    /// <summary>
    /// Represents a single item in <see cref="MetricTags"/> collection
    /// </summary>
    [PublicAPI]
    public class MetricTag
    {
        [NotNull]
        [ValueProvider("Vostok.Metrics.WellKnownTagKeys")]
        public string Key { get; }
        
        [NotNull]
        public string Value { get; }

        public MetricTag(
            [NotNull] [ValueProvider("Vostok.Metrics.WellKnownTagKeys")] string key,
            [NotNull] string value)
        {
            Key = key;
            Value = value;
        }
    }
}