using System;
using System.Threading;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Timer;
using Vostok.Metrics.Senders;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class ITimerExtensions_Measurement_Tests
    {
        [TestCase(WellKnownUnits.Seconds, 120)]
        [TestCase(WellKnownUnits.Minutes, 2)]
        public void Report_should_convert_timespan(string unit, double expected)
        {
            MetricEvent @event = null;
            var context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => @event = e)));
            var timer = context.CreateTimer(
                "name",
                new TimerConfig
                {
                    Unit = unit
                });

            timer.Report(120.Seconds());

            @event.Value.Should().Be(expected);
            @event.Unit.Should().Be(unit);
        }

        [Test]
        public void Report_should_convert_timespan_using_default_seconds()
        {
            MetricEvent @event = null;
            var context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => @event = e)));
            var timer = context.CreateTimer("name");

            timer.Report(13.Seconds());

            @event.Value.Should().Be(13);
            @event.Unit.Should().Be(WellKnownUnits.Seconds);
        }

        [Test]
        public void Report_should_throw_on_unknown_units()
        {
            var context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => {})));
            var timer = context.CreateTimer("name", new TimerConfig {Unit = WellKnownUnits.Bits});

            Action check = () => timer.Report(13.Seconds());

            check.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Measure_should_report_elapsed()
        {
            MetricEvent @event = null;
            var context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => @event = e)));
            var timer = context.CreateTimer("name");

            using (timer.Measure())
            {
                Thread.Sleep(0.1.Seconds());
            }
            
            @event.Value.Should().BeGreaterOrEqualTo(0.1);
        }
    }
}