
using System;
using JetBrains.Annotations;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.GaugePrimitive
{
    [PublicAPI]
    public static partial class MetricContextExtensionsGauge
    {
        /// <inheritdoc cref="IGauge"/>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="config">Optional config</param>
        public static IGauge Gauge(this IMetricContext context, string name, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Gauge(context, tags, config);
        }

        #region TaggedMetric

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by an instance of <typeparamref name="TFor"/>.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="typeTagsConverter">
        /// Optional custom mapping from <typeparamref name="TFor"/> to <see cref="Vostok.Metrics.Model.MetricTags"/>.
        /// These tags are specific for every Gauge in group and will be added after <paramref name="name"/> tag.
        /// </param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetricT<TFor, IGauge> Gauge<TFor>(this IMetricContext context, string name, ITypeTagsConverter<TFor> typeTagsConverter = null, GaugeConfig config = null)
        {
            config = config ?? GaugeConfig.Default;
            return new TaggedMetricT<TFor, IGauge>(CreateTagsFactory(context, name, config), typeTagsConverter);
        }         

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric1<IGauge> Gauge(this IMetricContext context, string name, string key1, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric2<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric3<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric4<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric5<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
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
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric6<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
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
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric7<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
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
        /// <inheritdoc cref="IGauge"/>
        public static ITaggedMetric8<IGauge> Gauge(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, GaugeConfig config = null)
        {
            return CreateTaggedMetric(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
        #endregion

        private static TaggedMetric<Gauge> CreateTaggedMetric(IMetricContext context, string name, GaugeConfig config = null, params string[] keys)
        {
            config = config ?? GaugeConfig.Default;
            return new TaggedMetric<Gauge>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Gauge> CreateTagsFactory(IMetricContext context, string name, GaugeConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Gauge(context, finalTags, config);
            };
        }
    }
}
