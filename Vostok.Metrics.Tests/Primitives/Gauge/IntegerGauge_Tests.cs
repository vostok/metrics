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
    internal class IntegerGauge_Tests
    {
        private MetricContext context;

        [SetUp]
        public void SetUp()
        {
            context = new MetricContext(
                new MetricContextConfig(new DevNullMetricEventSender())
                {
                    DefaultScrapePeriod = TimeSpan.MaxValue
                });
        }

        [Test]
        public void Should_calculate_sum_and_not_reset_on_scrape_if_not_specified()
        {
            var gauge = context.CreateIntegerGauge("name");
            gauge.Increment();
            gauge.Substract(2);
            gauge.Add(42);
            Scrape(gauge).Value.Should().Be(1 - 2 + 42);

            Scrape(gauge).Value.Should().Be(1 - 2 + 42);

            gauge.Add(123);
            Scrape(gauge).Value.Should().Be(1 - 2 + 42 + 123);
        }

        [Test]
        public void Should_calculate_sum_and_reset_on_scrape_if_specified()
        {
            var gauge = context.CreateIntegerGauge("name", new IntegerGaugeConfig { ResetOnScrape = true });
            gauge.Increment();
            gauge.Substract(2);
            gauge.Add(42);
            Scrape(gauge).Value.Should().Be(1 - 2 + 42);

            Scrape(gauge).Value.Should().Be(0);

            gauge.Add(123);
            Scrape(gauge).Value.Should().Be(123);
        }

        [Test]
        public void Should_be_settable()
        {
            var gauge = context.CreateIntegerGauge("name");
            gauge.Add(10);
            gauge.Set(5);
            gauge.Add(1);

            Scrape(gauge).Value.Should().Be(5 + 1);
        }

        [Test]
        public void Should_use_initial_value()
        {
            var gauge = context.CreateIntegerGauge("name", new IntegerGaugeConfig { InitialValue = 100 });
            gauge.Add(10);
            Scrape(gauge).Value.Should().Be(100 + 10);
        }

        [Test]
        public void Should_accept_negative_values()
        {
            var gauge = context.CreateIntegerGauge("name");
            gauge.Add(-100);
            gauge.Add(13);
            gauge.Add(-200);
            Scrape(gauge).Value.Should().Be(-100 + 13 - 200);
        }

        [Test]
        public void Should_be_thread_safe()
        {
            var n = 100_000L;
            var gauge = context.CreateIntegerGauge("name");
            Parallel.For(
                0,
                n + 1,
                new ParallelOptions { MaxDegreeOfParallelism = 4 },
                i => { gauge.Add(i); });

            // ReSharper disable once PossibleLossOfFraction
            Scrape(gauge).Value.Should().Be(n * (n + 1) / 2);
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var gauge = context.CreateIntegerGauge(
                "name",
                new IntegerGaugeConfig
                {
                    ScrapePeriod = TimeSpan.MaxValue,
                    Unit = "unit",
                    InitialValue = 100
                });

            gauge.Add(42);

            var timestamp = DateTimeOffset.Now;
            var metric = Scrape(gauge, timestamp);

            metric.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        100 + 42,
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
            var x = 0L;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, (long)e.Value))));

            var gauge = (IntegerGauge)context.CreateIntegerGauge("name", new IntegerGaugeConfig { ScrapePeriod = 10.Milliseconds() });

            gauge.Add(1);
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(1);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var x = 0L;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, (long)e.Value))));

            var gauge = (IntegerGauge)context.CreateIntegerGauge("name", new IntegerGaugeConfig { ScrapePeriod = 10.Milliseconds() });

            gauge.Dispose();

            Thread.Sleep(100.Milliseconds());
            gauge.Add(1);
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(0);
        }

        private static MetricEvent Scrape(IIntegerGauge gauge, DateTimeOffset? timestamp = null)
        {
            return ((IntegerGauge)gauge).Scrape(timestamp ?? DateTimeOffset.Now).Single();
        }
    }
}