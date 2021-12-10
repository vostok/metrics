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
    internal class FloatingGauge_Tests
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
            var gauge = context.CreateFloatingGauge("name");
            gauge.Add(1.1);
            gauge.Substract(2.2);
            gauge.Add(42);
            Scrape(gauge).Value.Should().Be(1.1 - 2.2 + 42);

            Scrape(gauge).Value.Should().Be(1.1 - 2.2 + 42);

            gauge.Add(123);
            Scrape(gauge).Value.Should().Be(1.1 - 2.2 + 42 + 123);
        }

        [Test]
        public void Should_calculate_sum_and_reset_on_scrape_if_specified()
        {
            var gauge = context.CreateFloatingGauge("name", new FloatingGaugeConfig {ResetOnScrape = true});
            gauge.Add(1.1);
            gauge.Substract(2.2);
            gauge.Add(42);
            Scrape(gauge).Value.Should().Be(1.1 - 2.2 + 42);

            Scrape(gauge).Value.Should().Be(0);

            gauge.Add(123);
            Scrape(gauge).Value.Should().Be(0123);
        }

        [Test]
        public void Should_be_settable()
        {
            var gauge = context.CreateFloatingGauge("name");
            gauge.Add(10);
            gauge.Set(5);
            gauge.Add(1);

            Scrape(gauge).Value.Should().Be(5 + 1);
        }

        [Test]
        public void Should_use_initial_value()
        {
            var gauge = context.CreateFloatingGauge("name", new FloatingGaugeConfig {InitialValue = 100});
            gauge.Add(10);
            Scrape(gauge).Value.Should().Be(100 + 10);
        }

        [Test]
        public void Should_accept_negative_values()
        {
            var gauge = context.CreateFloatingGauge("name");
            gauge.Add(-100);
            gauge.Add(13);
            gauge.Add(-200);
            Scrape(gauge).Value.Should().Be(-100 + 13 - 200);
        }

        [Test]
        public void Should_be_thread_safe()
        {
            var n = 100_000L;
            var gauge = context.CreateFloatingGauge("name");
            Parallel.For(
                0,
                n + 1,
                new ParallelOptions {MaxDegreeOfParallelism = 4},
                i => { gauge.Add(i); });

            // ReSharper disable once PossibleLossOfFraction
            Scrape(gauge).Value.Should().Be(n * (n + 1) / 2);
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var gauge = context.CreateFloatingGauge(
                "name",
                new FloatingGaugeConfig
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
            var x = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, e.Value))));

            var gauge = (FloatingGauge)context.CreateFloatingGauge("name", new FloatingGaugeConfig {ScrapePeriod = 10.Milliseconds()});

            gauge.Add(1);
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(1);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var x = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, e.Value))));

            var gauge = (FloatingGauge)context.CreateFloatingGauge("name", new FloatingGaugeConfig {ScrapePeriod = 10.Milliseconds()});

            gauge.Dispose();

            Thread.Sleep(100.Milliseconds());
            gauge.Add(1);
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(0);
        }

        [Test]
        public void TryIncreaseTo_should_work_correctly()
        {
            var gauge = (FloatingGauge) context.CreateFloatingGauge("name");

            gauge.TryIncreaseTo(10);
            gauge.CurrentValue.Should().Be(10);

            gauge.TryIncreaseTo(9);
            gauge.CurrentValue.Should().Be(10);

            gauge.TryIncreaseTo(15);
            gauge.CurrentValue.Should().Be(15);

            gauge.TryIncreaseTo(-1);
            gauge.CurrentValue.Should().Be(15);
        }

        [Test]
        public void TryReduceTo_should_work_correctly()
        {
            var gauge = (FloatingGauge) context.CreateFloatingGauge("name");

            gauge.Set(100);

            gauge.TryReduceTo(10);
            gauge.CurrentValue.Should().Be(10);

            gauge.TryReduceTo(11);
            gauge.CurrentValue.Should().Be(10);

            gauge.TryReduceTo(5);
            gauge.CurrentValue.Should().Be(5);

            gauge.TryReduceTo(8);
            gauge.CurrentValue.Should().Be(5);
        }
        
        [Test]
        public void Should_not_send_initial_value_if_specified()
        {
            var gauge = context.CreateFloatingGauge("name", new FloatingGaugeConfig {InitialValue = 100.5, SendInitialValue = false});

            Scrape(gauge).Should().BeNull();
            
            gauge.Set(100.5);
            
            Scrape(gauge).Value.Should().Be(100.5);
        }

        private static MetricEvent Scrape(IFloatingGauge gauge, DateTimeOffset? timestamp = null)
        {
            return ((FloatingGauge)gauge).Scrape(timestamp ?? DateTimeOffset.Now).FirstOrDefault();
        }
    }
}