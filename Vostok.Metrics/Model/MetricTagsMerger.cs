using JetBrains.Annotations;

namespace Vostok.Metrics.Model
{
    [PublicAPI]
    public static class MetricTagsMerger
    {
        /// <summary>
        /// <para>Appends a <see cref="WellKnownTagKeys.Name"/> tag with value taken from given <paramref name="name"/> parameter to given <paramref name="contextTags"/>.</para>
        /// <para>Then appends <paramref name="dynamicTags"/> after <see cref="WellKnownTagKeys.Name"/> and returns the result.</para>
        /// </summary>
        [NotNull]
        public static MetricTags Merge([NotNull] MetricTags contextTags, [NotNull] string name, [NotNull] MetricTags dynamicTags)
            => contextTags
                .Append(WellKnownTagKeys.Name, name)
                .Append(dynamicTags);

        /// <summary>
        /// Appends a <see cref="WellKnownTagKeys.Name"/> tag with value taken from given <paramref name="name"/> parameter to given <paramref name="contextTags"/> and returns resulting tags.
        /// </summary>
        [NotNull]
        public static MetricTags Merge([NotNull] MetricTags contextTags, [NotNull] string name)
            => contextTags.Append(WellKnownTagKeys.Name, name);
    }
}
