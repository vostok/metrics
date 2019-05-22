using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    internal class FuncGauge : IScrapableMetric, IDisposable
    {
        private readonly Func<double> getValue;
        private readonly MetricTags tags;
        private readonly FuncGaugeConfig config;
        private readonly IDisposable registration;

        public FuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] Func<double> getValue,
            [NotNull] FuncGaugeConfig config)
        {
            this.getValue = getValue ?? throw new ArgumentNullException(nameof(getValue));
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            yield return new MetricEvent(getValue(), tags, timestamp, config.Unit, null, null);
        }

        public void Dispose()
            => registration.Dispose();
    }
}
