using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Vostok.Metrics.Model;

namespace Vostok.Metrics.Primitives.GaugeImpl
{
    internal class FuncGauge : IScrapableMetric, IDisposable
    {
        private readonly Func<double> getValue;
        private readonly MetricTags tags;
        private readonly GaugeConfig config;
        private readonly IDisposable registration;

        public FuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] MetricTags tags,
            [NotNull] Func<double> getValue,
            [NotNull] GaugeConfig config)
        {
            this.getValue = getValue;
            this.tags = tags;
            this.config = config;

            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricSample> Scrape()
        {
            var value = getValue();
            var result = new MetricSample(
                value,
                tags,
                DateTimeOffset.UtcNow,
                config.Unit,
                null);
            yield return result;
        }

        public void Dispose()
        {
            registration.Dispose();
        }
    }
}