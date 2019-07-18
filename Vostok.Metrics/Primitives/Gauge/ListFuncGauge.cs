using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Primitives.Gauge
{
    internal class ListFuncGauge : IScrapableMetric, IListFuncGauge
    {
        private readonly IMetricContext context;
        private readonly string name;
        private readonly Func<IEnumerable<object>> valuesProvider;
        private readonly ListFuncGaugeConfig config;
        private readonly IDisposable registration;

        public ListFuncGauge(
            [NotNull] IMetricContext context,
            [NotNull] string name,
            [NotNull] Func<IEnumerable<object>> valuesProvider,
            [NotNull] ListFuncGaugeConfig config)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.valuesProvider = valuesProvider ?? throw new ArgumentNullException(nameof(valuesProvider));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            registration = context.Register(this, config.ScrapePeriod);
        }

        public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
        {
            var values = valuesProvider()?.Where(v => v != null) ?? new List<object>();
            Exception error = null;

            foreach (var value in values)
            {
                var metricValue = MetricValueExtractor.ExtractValue(value);

                if (metricValue != null)
                {
                    yield return new MetricEvent(
                        metricValue.Value,
                        MetricTagsMerger.Merge(context.Tags, name, MetricTagsExtractor.ExtractTags(value)),
                        timestamp,
                        config.Unit,
                        null,
                        null);
                }
                else
                {
                    error = new ArgumentException($"Model type '{value.GetType()}' doesn't have any public properties marked with '{typeof(MetricValueAttribute).Name}'.");
                }
            }

            if (error != null)
                throw error;
        }

        public void Dispose()
            => registration.Dispose();
    }
}