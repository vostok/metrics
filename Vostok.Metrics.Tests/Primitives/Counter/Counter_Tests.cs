using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Counter;
using Vostok.Metrics.Senders;

namespace Vostok.Metrics.Tests.Primitives.Counter
{
    [TestFixture]
    internal class Counter_Tests
    {
        private MetricContext context;

        [SetUp]
        public void SetUp()
        {
            context = new MetricContext(
                new MetricContextConfig(new DevNullMetricEventSender()));
        }

        [Test]
        public void Should_calculate_sum_and_reset_on_scrape()
        {
            var counter = context.CreateCounter("name", new CounterConfig {ScrapePeriod = TimeSpan.MaxValue});
            counter.Add(1);
            counter.Add(2);
            counter.Add(42);
            Scrape(counter).Value.Should().Be(1 + 2 + 42);

            Scrape(counter).Value.Should().Be(0);

            counter.Add(123);
            Scrape(counter).Value.Should().Be(123);
        }

        [Test]
        public void Should_not_send_zero_values_if_specified()
        {
            var counter = context.CreateCounter("name", new CounterConfig {ScrapePeriod = TimeSpan.MaxValue, SendZeroValues = false});
            counter.Add(42);

            ((Metrics.Primitives.Counter.Counter)counter).Scrape(DateTimeOffset.Now).Should().NotBeEmpty();

            ((Metrics.Primitives.Counter.Counter)counter).Scrape(DateTimeOffset.Now).Should().BeEmpty();
        }

        [Test]
        public void Should_reject_negative_values()
        {
            var counter = context.CreateCounter("name", new CounterConfig {ScrapePeriod = TimeSpan.MaxValue});
            Action check = () => counter.Add(-1);
            check.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Should_be_thread_safe()
        {
            var n = 100_000L;
            var counter = context.CreateCounter("name", new CounterConfig {ScrapePeriod = TimeSpan.MaxValue});
            Parallel.For(
                0,
                n + 1,
                new ParallelOptions {MaxDegreeOfParallelism = 4},
                i => { counter.Add(i); });

            // ReSharper disable once PossibleLossOfFraction
            Scrape(counter).Value.Should().Be(n * (n + 1) / 2);
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var aggregationParameters = new Dictionary<string, string>
            {
                {"a", "aa"},
                {"b", "bb"}
            };

            var counter = context.CreateCounter(
                "name",
                new CounterConfig
                {
                    ScrapePeriod = TimeSpan.MaxValue,
                    Unit = "unit",
                    AggregationParameters = aggregationParameters
                });

            counter.Add(42);

            var timestamp = DateTimeOffset.Now;
            var metric = Scrape(counter, timestamp);

            metric.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        42,
                        new MetricTags(new MetricTag(WellKnownTagKeys.Name, "name")),
                        timestamp,
                        "unit",
                        WellKnownAggregationTypes.Counter,
                        aggregationParameters
                    ));
        }

        [Test]
        public void Should_be_auto_scrapable()
        {
            var sum = 0L;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Add(ref sum, (long)e.Value))));

            var counter = (Metrics.Primitives.Counter.Counter)context.CreateCounter("name", new CounterConfig {ScrapePeriod = 10.Milliseconds()});

            counter.Add(1);
            Thread.Sleep(300.Milliseconds());

            sum.Should().Be(1);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var sum = 0L;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Add(ref sum, (long)e.Value))));

            var counter = (Metrics.Primitives.Counter.Counter)context.CreateCounter("name", new CounterConfig {ScrapePeriod = 10.Milliseconds()});

            counter.Dispose();

            Thread.Sleep(100.Milliseconds());
            counter.Add(1);
            Thread.Sleep(300.Milliseconds());

            sum.Should().Be(0);
        }

        private static MetricEvent Scrape(ICounter counter, DateTimeOffset? timestamp = null)
        {
            return ((Metrics.Primitives.Counter.Counter)counter).Scrape(timestamp ?? DateTimeOffset.Now).Single();
        }
    }
}