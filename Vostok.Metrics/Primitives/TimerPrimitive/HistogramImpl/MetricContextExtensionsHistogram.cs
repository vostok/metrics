
using System;
using JetBrains.Annotations;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl
{
    [PublicAPI]
    public static partial class MetricContextExtensionsHistogram
    {
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="config">Optional config</param>
        public static ITimer Histogram(this IMetricContext context, string name, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Histogram(context, tags, config);
        }

        #region TaggedMetric

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by an instance of <typeparamref name="TFor"/>.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="typeTagsConverter">
        /// Optional custom mapping from <typeparamref name="TFor"/> to <see cref="Vostok.Metrics.Model.MetricTags"/>.
        /// These tags are specific for every Histogram in group and will be added after <paramref name="name"/> tag.
        /// </param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetricT<TFor, ITimer> Histogram<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter = null, HistogramConfig config = null)
        {
            config = config ?? HistogramConfig.Default;
            return new TaggedMetricT<TFor, ITimer>(CreateTagsFactory(context, name, config), typeTagsConverter);
        }         

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric1<ITimer> Histogram(this IMetricContext context, string name, string key1, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric2<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric3<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric4<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric5<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="key6">Key of 6 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric6<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="key6">Key of 6 dynamic tag</param>
        /// <param name="key7">Key of 7 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric7<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram">Histograms</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="key6">Key of 6 dynamic tag</param>
        /// <param name="key7">Key of 7 dynamic tag</param>
        /// <param name="key8">Key of 8 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.HistogramImpl.Histogram"/>
        public static ITaggedMetric8<ITimer> Histogram(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, HistogramConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
        #endregion

        private static TaggedMetric<Histogram> CreateTaggedMetric(IMetricContext context, string name, HistogramConfig config = null, params string[] keys)
        {
            config = config ?? HistogramConfig.Default;
            return new TaggedMetric<Histogram>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Histogram> CreateTagsFactory(IMetricContext context, string name, HistogramConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Histogram(context, finalTags, config);
            };
        }
    }
}
