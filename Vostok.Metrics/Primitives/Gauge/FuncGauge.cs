using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    internal class FuncGauge : IScrapableMetric, IFuncGauge
    {
        private readonly MetricTags tags;
        private readonly FuncGaugeConfig config;
        private readonly IDisposable registration;
        private readonly IMetricContext context;
        private volatile Func<double> valueProvider;

        private static readonly ConcurrentDictionary<(IMetricContext, MetricTags), FuncGauge> Cache = new ConcurrentDictionary<(IMetricContext, MetricTags), FuncGauge>();

        public static FuncGauge GetOrCreate(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [CanBeNull] Func<double> valueProvider,
            [NotNull] FuncGaugeConfig config)
        {
            var key = (context, tags);
            if (Cache.TryGetValue(key, out var result))
                return result;

            var lazy = new Lazy<FuncGauge>(
                () => new FuncGauge(context, tags, valueProvider, config),
                LazyThreadSafetyMode.ExecutionAndPublication);

            return Cache.GetOrAdd(key, _ => lazy.Value);
        }

        private FuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [CanBeNull] Func<double> valueProvider,
            [NotNull] FuncGaugeConfig config)
        {
            this.context = context;
            this.valueProvider = valueProvider;
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            var provider = valueProvider;
            if (provider != null)
                yield return new MetricEvent(provider(), tags, timestamp, config.Unit, null, null);
        }

        // ReSharper disable once ParameterHidesMember
        public void SetValueProvider(Func<double> valueProvider)
        {
            this.valueProvider = valueProvider;
        }

        public void Dispose()
        {
            registration.Dispose();
            Cache.TryRemove((context, tags), out _);
        }
    }
}