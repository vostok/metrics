using System;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics
{
    [PublicAPI]
    public static class IMetricContextExtensionsTags
    {
        /// <summary>
        /// <para>Creates child <see cref="IMetricContext"/> with additional tags.</para>
        /// </summary>
        public static IMetricContext WithTag(this IMetricContext context, string key, string value)
        {
            return context.OverwriteTags(context.Tags.Append(key, value));
        }

        /// <inheritdoc cref="WithTag(Vostok.Metrics.IMetricContext,string,string)"/>
        public static IMetricContext WithTag(this IMetricContext context, MetricTag tag)
        {
            return context.OverwriteTags(context.Tags.Append(tag));
        }

        /// <inheritdoc cref="WithTag(Vostok.Metrics.IMetricContext,string,string)"/>
        public static IMetricContext WithTags(this IMetricContext context, MetricTags tags)
        {
            return context.OverwriteTags(context.Tags.Append(tags));
        }

        private static IMetricContext OverwriteTags(this IMetricContext context, MetricTags newTags)
        {
            return new OverwriteTagsWrapper(context, newTags);
        }
        
        private class OverwriteTagsWrapper : IMetricContext
        {
            private readonly IMetricContext context;

            public OverwriteTagsWrapper(IMetricContext context, MetricTags tags)
            {
                this.context = context;
                Tags = tags;
            }

            public MetricTags Tags { get; }

            public IDisposable Register(IScrapableMetric metric, TimeSpan? scrapePeriod) =>
                context.Register(metric, scrapePeriod);

            public void Send(MetricEvent @event)
            {
                context.Send(@event);
            }
        }
    }
}