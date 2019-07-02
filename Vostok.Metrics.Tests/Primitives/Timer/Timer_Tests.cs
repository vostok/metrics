using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Timer;
using Vostok.Metrics.Senders;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class Timer_Tests
    {
        [Test]
        public void Should_report_metric_event()
        {
            var aggregationParameters = new Dictionary<string, string>
            {
                {"a", "aa"},
                {"b", "bb"}
            };

            MetricEvent @event = null;
            var context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => @event = e)));
            var timer = context.CreateTimer(
                "name",
                new TimerConfig
                {
                    AggregationParameters = aggregationParameters,
                    Unit = "unit"
                });

            timer.Report(42);

            @event.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        42,
                        new MetricTags(new MetricTag(WellKnownTagKeys.Name, "name")),
                        @event.Timestamp,
                        "unit",
                        WellKnownAggregationTypes.Timer,
                        aggregationParameters
                    ));
        }
    }
}