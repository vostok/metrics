using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    internal class MultiFuncGauge : IScrapableMetric, IDisposable
    {
        private readonly MetricTags contextTags;
        private readonly FuncGaugeConfig config;
        private readonly IDisposable registration;
        private readonly Func<IEnumerable<MetricDataPoint>> pointsProvider;

        public MultiFuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] Func<IEnumerable<MetricDataPoint>> pointsProvider,
            [NotNull] FuncGaugeConfig config)
        {
            this.pointsProvider = pointsProvider ?? throw new ArgumentNullException(nameof(pointsProvider));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            contextTags = context.Tags;
            registration = context.Register(this, config.ScrapePeriod);
        }

        public void Dispose()
            => registration.Dispose();

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
            => pointsProvider().Select(point => CreateEvent(point, timestamp));

        private MetricEvent CreateEvent(MetricDataPoint point, DateTimeOffset timestamp)
        {
            point.Timestamp = timestamp;
            point.Unit = point.Unit ?? config.Unit;

            return point.ToMetricEvent(contextTags);
        }
    }
}