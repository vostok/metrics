using System;
using JetBrains.Annotations;

namespace Vostok.Metrics.Primitives.Gauge
{
    /// <summary>
    /// <para>
    /// Gauge metric represents an arbitrary value.
    /// </para>
    /// <para>
    /// The value of Gauge is observed every <see cref="GaugeConfig.ScrapePeriod"/> and saved to a permanent storage without any aggregation process.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// To create a Gauge use extensions (<see cref="MetricContextExtensionsGauge">1</see>, <see cref="FuncGaugeFactoryExtensions">2</see>) for <see cref="IMetricContext"/>.
    /// </para>
    /// <para>
    /// You can call <see cref="IDisposable.Dispose"/> to stop observing Gauge values.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// You can use Gauge to send system metrics (like CPU usage).
    /// Your app <see cref="FuncGaugeFactoryExtensions.Gauge">creates</see> a Gauge.
    /// <code>
    /// var gauge = context.Gauge(
    ///     "cpu-usage",
    ///     () => GetCpuUsage(),
    ///     new GaugeConfig { ScrapePeriod = TimeSpan.FromSeconds(10) });
    /// </code>
    /// Every 10 seconds <c>GetCpuUsage</c> is called and returned value is saved to a permanent storage.
    /// </para>
    /// </example>
    /// <example>
    /// <para>
    /// Another example is the number of concurrent requests to your service split by the identity of a client.
    /// <code>
    /// var gauge = context.Gauge(
    ///     "concurrent-requests",
    ///     "client-id");
    ///
    /// 
    /// gauge.For("fat-service").Inc();
    /// ... // process request
    /// gauge.For("fat-service").Dec(); 
    /// </code>
    /// </para>
    /// </example>
    [PublicAPI]
    public interface IGauge : IDisposable
    {
        void Set(double value);
        void Add(double value);
    }
}