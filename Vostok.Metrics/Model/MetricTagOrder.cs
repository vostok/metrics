using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Vostok.Metrics.Grouping;

namespace Vostok.Metrics.Model
{
    /// <summary>
    /// <para><see cref="MetricTagAttribute"/> marks a property of the model object as a source of metric tag for <see cref="IMetricGroup{TFor,TMetric}"/>.</para>
    /// <para>There's an option to explicitly specify order in which properties will be converted tags.</para>
    /// <para>If there's no explicit order, properties will be enumerated in declaration order.</para>
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property)]
    public class MetricTagAttribute : Attribute
    {
        public MetricTagAttribute([CallerLineNumber] int order = 0)
            => Order = order;

        public int Order { get; }
    }
}