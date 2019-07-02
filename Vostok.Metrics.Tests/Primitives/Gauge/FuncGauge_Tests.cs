using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Gauge;
using Vostok.Metrics.Senders;

namespace Vostok.Metrics.Tests.Primitives.Gauge
{
    [TestFixture]
    internal class FuncGauge_Tests
    {
        private MetricContext context;
        private double value;
        private Func<double> func;

        [SetUp]
        public void SetUp()
        {
            context = new MetricContext(
                new MetricContextConfig(new DevNullMetricEventSender())
                {
                    DefaultScrapePeriod = TimeSpan.MaxValue
                });

            func = () => value;
        }

        [Test]
        public void Should_scrape_func_value()
        {
            var gauge = context.CreateFuncGauge("name", func);

            value = 1;
            Scrape(gauge).Value.Should().Be(1);

            value = 2;
            Scrape(gauge).Value.Should().Be(2);
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var gauge = context.CreateFuncGauge(
                "name",
                func,
                new FuncGaugeConfig
                {
                    ScrapePeriod = TimeSpan.MaxValue,
                    Unit = "unit"
                });

            value = 42;

            var timestamp = DateTimeOffset.Now;
            var metric = Scrape(gauge, timestamp);

            metric.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        42,
                        new MetricTags(new MetricTag(WellKnownTagKeys.Name, "name")),
                        timestamp,
                        "unit",
                        null,
                        null
                    ));
        }

        [Test]
        public void Should_be_auto_scrapable()
        {
            var x = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, e.Value))));

            context.CreateFuncGauge("name", func, new FuncGaugeConfig { ScrapePeriod = 10.Milliseconds() });

            value = 1;
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(1);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var x = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, e.Value))));

            var gauge = context.CreateFuncGauge("name", func, new FuncGaugeConfig { ScrapePeriod = 10.Milliseconds() });

            gauge.Dispose();

            Thread.Sleep(100.Milliseconds());
            value = 2;
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(0);
        }

        private static MetricEvent Scrape(IFuncGauge gauge, DateTimeOffset? timestamp = null)
        {
            return ((FuncGauge)gauge).Scrape(timestamp ?? DateTimeOffset.Now).Single();
        }
    }
}