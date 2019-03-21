using System;
using JetBrains.Annotations;
using Vostok.Metrics.DynamicTags.StringKeys;
using Vostok.Metrics.DynamicTags.Typed;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.CounterImpl
{
    [PublicAPI]
    public static partial class MetricContextExtensionsCounter
    {
        /// <inheritdoc cref="ICounter"/>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="config">Optional config</param>
        public static ICounter Counter(this IMetricContext context, string name, CounterConfig config = null)
        {
            config = config ?? CounterConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Counter(context, tags, config);
        }

        #region TaggedMetric

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by an instance of <typeparamref name="TFor"/>.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="typeTagsConverter">
        /// Optional custom mapping from <typeparamref name="TFor"/> to <see cref="Vostok.Metrics.Model.MetricTags"/>.
        /// These tags are specific for every Counter in group and will be added after <paramref name="name"/> tag.
        /// </param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetricT<TFor, ICounter> Counter<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter = null, CounterConfig config = null)
        {
            config = config ?? CounterConfig.Default;
            return new TaggedMetricT<TFor, ICounter>(CreateTagsFactory(context, name, config), typeTagsConverter);
        }         

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric1<ICounter> Counter(this IMetricContext context, string name, string key1, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric2<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric3<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric4<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric5<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="key6">Key of 6 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric6<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="key6">Key of 6 dynamic tag</param>
        /// <param name="key7">Key of 7 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric7<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="ICounter">Counters</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="ICounter"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownConstants.MetricTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="key6">Key of 6 dynamic tag</param>
        /// <param name="key7">Key of 7 dynamic tag</param>
        /// <param name="key8">Key of 8 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="ICounter"/>
        public static ITaggedMetric8<ICounter> Counter(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, CounterConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
        #endregion

        private static TaggedMetric<Counter> CreateTaggedMetric(IMetricContext context, string name, CounterConfig config = null, params string[] keys)
        {
            config = config ?? CounterConfig.Default;
            return new TaggedMetric<Counter>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Counter> CreateTagsFactory(IMetricContext context, string name, CounterConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Counter(context, finalTags, config);
            };
        }
    }
}