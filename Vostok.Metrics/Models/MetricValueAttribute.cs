using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Models
{
    /// <summary>
    /// <para><see cref="MetricValueAttribute"/> marks a public property of the model object as a source of value <see cref="MetricEvent"/>.</para>
    /// </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property)]
    public class MetricValueAttribute : Attribute
    {
    }
}