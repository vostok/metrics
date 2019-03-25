
using System;
using JetBrains.Annotations;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl
{
    [PublicAPI]
    public static partial class MetricContextExtensionsTimer
    {
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="config">Optional config</param>
        public static ITimer Timer(this IMetricContext context, string name, TimerConfig config = null)
        {
            config = config ?? TimerConfig.Default;
            var tags = MetricTagsMerger.Merge(context.Tags, name);
            return new Timer(context, tags, config);
        }

        #region IMetricGroup

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by an instance of <typeparamref name="TFor"/>.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// Optional custom mapping from <typeparamref name="TFor"/> to <see cref="Vostok.Metrics.Model.MetricTags"/>.
        /// These tags are specific for every Timer in group and will be added after <paramref name="name"/> tag.
        /// </param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup<TFor, ITimer> Timer<TFor>(this IMetricContext context, string name, TimerConfig config = null)
        {
            config = config ?? TimerConfig.Default;
            return new MetricGroup<TFor, ITimer>(CreateTagsFactory(context, name, config));
        }         

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup1<ITimer> Timer(this IMetricContext context, string name, string key1, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup2<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup3<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, string key3, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2, key3);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup4<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, string key3, string key4, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2, key3, key4);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        /// </summary>
        /// <param name="context">Context this metric will belong to</param>
        /// <param name="name">The name of the metric. It will be added to <see cref="Vostok.Metrics.Model.MetricSample.Tags"/> with key <see cref="Vostok.Metrics.WellKnownTagKeys.Name"/></param>
        /// <param name="key1">Key of 1 dynamic tag</param>
        /// <param name="key2">Key of 2 dynamic tag</param>
        /// <param name="key3">Key of 3 dynamic tag</param>
        /// <param name="key4">Key of 4 dynamic tag</param>
        /// <param name="key5">Key of 5 dynamic tag</param>
        /// <param name="config">Optional config</param>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup5<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
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
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup6<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5, key6);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
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
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup7<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5, key6, key7);
        }

        /// <summary>
        /// <para>
        /// Creates a group of <see cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer">Timers</see>.
        /// Metrics in the group share the <paramref name="name"/> but have different dynamic tags.
        /// </para>
        /// <para>
        /// Dynamic tags are specified by string parameters. You define the keys now and pass the values later.
        /// </para>
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
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
        /// <inheritdoc cref="Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl.Timer"/>
        public static IMetricGroup8<ITimer> Timer(this IMetricContext context, string name, string key1, string key2, string key3, string key4, string key5, string key6, string key7, string key8, TimerConfig config = null)
        {
            return CreateMetricGroup(context, name, config, key1, key2, key3, key4, key5, key6, key7, key8);
        }
        #endregion

        private static MetricGroup<Timer> CreateMetricGroup(IMetricContext context, string name, TimerConfig config = null, params string[] keys)
        {
            config = config ?? TimerConfig.Default;
            return new MetricGroup<Timer>(CreateTagsFactory(context, name, config), keys);
        }

        private static Func<MetricTags, Timer> CreateTagsFactory(IMetricContext context, string name, TimerConfig config)
        {
            return tags =>
            {
                var finalTags = MetricTagsMerger.Merge(context.Tags, name, tags);
                return new Timer(context, finalTags, config);
            };
        }
    }
}
