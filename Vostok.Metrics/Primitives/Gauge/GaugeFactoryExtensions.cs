
using System;
using JetBrains.Annotations;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.Gauge
{
    [PublicAPI]
    public static class GaugeFactoryExtensions
    {
        /// <inheritdoc cref="IGauge"/>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="config">Optional metric-specific config.</param>
        [NotNull]
		public static IGauge Gauge([NotNull] this IMetricContext context, [NotNull] string name, [CanBeNull] GaugeConfig config = null)
            => new Gauge(context, MetricTagsMerger.Merge(context.Tags, name), config ?? GaugeConfig.Default);

        #region Metric group extensions

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by an instance of <typeparamref name="TFor"/>.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="config">Optional metric-specific config.</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup<TFor, IGauge> Gauge<TFor>([NotNull] this IMetricContext context, [NotNull] string name, [CanBeNull] GaugeConfig config = null)
            => new MetricGroup<TFor, IGauge>(CreateTagsFactory(context, name, config ?? GaugeConfig.Default));

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup1<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup2<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="key3">Key of dynamic tag number 3.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup3<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [NotNull] string key3, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2, key3);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="key3">Key of dynamic tag number 3.</param>
        /// <param name="key4">Key of dynamic tag number 4.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup4<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [NotNull] string key3, [NotNull] string key4, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2, key3, key4);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="key3">Key of dynamic tag number 3.</param>
        /// <param name="key4">Key of dynamic tag number 4.</param>
        /// <param name="key5">Key of dynamic tag number 5.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup5<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [NotNull] string key3, [NotNull] string key4, [NotNull] string key5, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="key3">Key of dynamic tag number 3.</param>
        /// <param name="key4">Key of dynamic tag number 4.</param>
        /// <param name="key5">Key of dynamic tag number 5.</param>
        /// <param name="key6">Key of dynamic tag number 6.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup6<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [NotNull] string key3, [NotNull] string key4, [NotNull] string key5, [NotNull] string key6, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5, key6);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="key3">Key of dynamic tag number 3.</param>
        /// <param name="key4">Key of dynamic tag number 4.</param>
        /// <param name="key5">Key of dynamic tag number 5.</param>
        /// <param name="key6">Key of dynamic tag number 6.</param>
        /// <param name="key7">Key of dynamic tag number 7.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup7<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [NotNull] string key3, [NotNull] string key4, [NotNull] string key5, [NotNull] string key6, [NotNull] string key7, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5, key6, key7);

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="IGauge">Gauges</see>.
        /// Metrics in the group share the same context tags and <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. Define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="IGauge"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to.</param>
        /// <param name="name">Name of the metric. It will be added to event's <see cref="MetricEvent.Tags"/> with key set to <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/>.</param>
        /// <param name="key1">Key of dynamic tag number 1.</param>
        /// <param name="key2">Key of dynamic tag number 2.</param>
        /// <param name="key3">Key of dynamic tag number 3.</param>
        /// <param name="key4">Key of dynamic tag number 4.</param>
        /// <param name="key5">Key of dynamic tag number 5.</param>
        /// <param name="key6">Key of dynamic tag number 6.</param>
        /// <param name="key7">Key of dynamic tag number 7.</param>
        /// <param name="key8">Key of dynamic tag number 8.</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="IGauge"/>
		[NotNull]
        public static IMetricGroup8<IGauge> Gauge([NotNull] this IMetricContext context, [NotNull] string name, [NotNull] string key1, [NotNull] string key2, [NotNull] string key3, [NotNull] string key4, [NotNull] string key5, [NotNull] string key6, [NotNull] string key7, [NotNull] string key8, [CanBeNull] GaugeConfig config = null)
            => CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        #endregion

		#region Helper methods

        private static MetricGroup<Gauge> CreateMetricGroup(IMetricContext context, string name, GaugeConfig config = null, params string[] keys)
            => new MetricGroup<Gauge>(CreateTagsFactory(context, name, config ?? GaugeConfig.Default), keys);

        private static Func<MetricTags, Gauge> CreateTagsFactory(IMetricContext context, string name, GaugeConfig config)
            => tags => new Gauge(context, MetricTagsMerger.Merge(context.Tags, name, tags), config);

		#endregion
    }
}
