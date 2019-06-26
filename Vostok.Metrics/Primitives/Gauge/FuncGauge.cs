using System;
using System.Collections.Generic;
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
        private volatile Func<double> valueProvider;

        public FuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] Func<double> valueProvider,
            [NotNull] FuncGaugeConfig config)
        {
            this.valueProvider = valueProvider ?? throw new ArgumentNullException(nameof(valueProvider));
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);
        }

        public FuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] FuncGaugeConfig config)
        {
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
            => registration.Dispose();
    }
}