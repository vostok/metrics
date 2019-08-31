using System;
using JetBrains.Annotations;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Primitives.Reporter
{
    internal class Reporter : IReporter
    {
        private readonly IMetricContext context;
        private readonly MetricTags tags;
        private readonly ReporterConfig config;

        public Reporter([NotNull] IMetricContext context, [NotNull] MetricTags tags, [NotNull] ReporterConfig config)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Report(double value)
            => context.Send(new MetricEvent(value, tags, config.Timestamp ?? DateTimeOffset.Now, config.Unit, null, null));
    }
}