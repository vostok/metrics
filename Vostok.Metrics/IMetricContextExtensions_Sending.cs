using System;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics
{
    [PublicAPI]
    public static class IMetricContextExtensions_Sending
    {
        /// <summary>
        /// Converts given <see cref="MetricDataPoint"/> to a <see cref="MetricEvent"/> using given <paramref name="context"/>'s tags and sends it for further processing.
        /// </summary>
        public static void Send([NotNull] this IMetricContext context, [NotNull] MetricDataPoint point)
            => context.Send(point.ToMetricEvent(context.Tags));

        public static void SendAnnotation(
            [NotNull] this IMetricContext context, 
            [NotNull] string description, 
            [NotNull] params (string key, string value)[] tags)
            => context.Send(CreateAnnotationEvent(context.Tags, tags, description));

        public static void SendAnnotation(
            [NotNull] this IMetricContext context,
            [NotNull] params (string key, string value)[] tags)
            => context.Send(CreateAnnotationEvent(context.Tags, tags, null));

        private static AnnotationEvent CreateAnnotationEvent(
            [NotNull] MetricTags contextTags, 
            [NotNull] (string key, string value)[] customTags,
            [CanBeNull] string description)
        {
            var eventTags = contextTags.Append(customTags.Select(pair => new MetricTag(pair.key, pair.value)).ToArray());

            return new AnnotationEvent(DateTimeOffset.Now, eventTags, description);
        }
    }
}