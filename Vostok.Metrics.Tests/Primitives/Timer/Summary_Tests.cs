using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Timer;
using Vostok.Metrics.Senders;
using ITimer = Vostok.Metrics.Primitives.Timer.ITimer;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class Summary_Tests
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
        public void Should_calculate_quantiles_and_reset_on_scrape()
        {
            var summary = context.CreateSummary("name");
            summary.Report(10);
            summary.Report(5);
            summary.Report(0);

            Scrape(summary, "p50").Value.Should().Be(5);

            Scrape(summary, "p50").Should().BeNull();

            summary.Report(42);
            Scrape(summary, "p50").Value.Should().Be(42);
        }

        [Test]
        public void Should_calculate_quantiles_given_in_config()
        {
            var summary = context.CreateSummary("name", new SummaryConfig {Quantiles = new[] {0.17}});
            for (var i = 0; i < 100; i++)
                summary.Report(i);

            Scrape(summary, "p17").Value.Should().Be(17);
        }

        [Test]
        public void Should_keep_only_some_values()
        {
            var summary = context.CreateSummary("name", new SummaryConfig {BufferSize = 10});
            for (var i = 0; i < 100; i++)
                summary.Report(i);

            Scrape(summary, "count").Value.Should().Be(100);
        }

        [Test]
        public void Should_be_thread_safe()
        {
            var n = 1_000L;
            var summary = context.CreateSummary("name");
            Parallel.For(
                0,
                n + 1,
                new ParallelOptions {MaxDegreeOfParallelism = 4},
                i => { summary.Report(i); });

            // ReSharper disable once PossibleLossOfFraction
            Scrape(summary, "avg").Value.Should().Be(n / 2);
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var summary = context.CreateSummary(
                "name",
                new SummaryConfig
                {
                    Unit = "unit"
                });

            summary.Report(42);

            var timestamp = DateTimeOffset.Now;
            var metric = Scrape(summary, "p50", timestamp);

            metric.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        42,
                        new MetricTags(new MetricTag(WellKnownTagKeys.Name, "name"), new MetricTag(WellKnownTagKeys.Aggregate, "p50")),
                        timestamp,
                        "unit",
                        null,
                        null
                    ));
        }

        [Test]
        public void Should_be_auto_scrapable()
        {
            var sum = 0.0;
            context = new MetricContext(
                new MetricContextConfig(
                    new AdHocMetricEventSender(
                        e =>
                        {
                            if (e.Tags.Any(t => t.Value == "avg"))
                                sum += e.Value;
                        })));

            var summary = (Summary)context.CreateSummary("name", new SummaryConfig {ScrapePeriod = 10.Milliseconds()});

            summary.Report(42);
            Thread.Sleep(300.Milliseconds());

            sum.Should().Be(42);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var sum = 0.0;
            context = new MetricContext(
                new MetricContextConfig(
                    new AdHocMetricEventSender(
                        e =>
                        {
                            if (e.Tags.Any(t => t.Value == "avg"))
                                sum += e.Value;
                        })));

            var summary = (Summary)context.CreateSummary("name", new SummaryConfig {ScrapePeriod = 10.Milliseconds()});

            summary.Dispose();

            Thread.Sleep(100.Milliseconds());
            summary.Report(42);
            Thread.Sleep(300.Milliseconds());

            sum.Should().Be(0);
        }

        private static MetricEvent Scrape(ITimer summary, string tag, DateTimeOffset? timestamp = null)
        {
            return ((Summary)summary).Scrape(timestamp ?? DateTimeOffset.Now).FirstOrDefault(e => e.Tags.Any(t => t.Value == tag));
        }
    }
}