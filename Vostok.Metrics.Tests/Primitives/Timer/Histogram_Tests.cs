using System;
using System.Collections.Generic;
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

// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable PossibleInvalidOperationException

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    [TestFixture]
    internal class Histogram_Tests
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
        public void Should_calculate_buckets_and_reset_on_scrape()
        {
            var histogram = context.CreateHistogram("name", new HistogramConfig {Buckets = new HistogramBuckets(10, 20, 30)});
            for (var i = -2; i < 50; i++)
                histogram.Report(i);

            Scrape(histogram)
                .Should()
                .BeEquivalentTo(
                    new List<(double, double)>
                    {
                        (13, 10),
                        (10, 20),
                        (10, 30),
                        (19, double.PositiveInfinity)
                    });

            Scrape(histogram)
                .Should()
                .BeEquivalentTo(
                    new List<(double, double)>());

            histogram.Report(42);
            Scrape(histogram)
                .Should()
                .BeEquivalentTo(
                    new List<(double, double)>
                    {
                        (1, double.PositiveInfinity)
                    });
        }

        [Test]
        public void Should_be_thread_safe()
        {
            var n = 1_000L;
            var histogram = context.CreateHistogram("name", new HistogramConfig {Buckets = new HistogramBuckets(100, 200, 300)});
            Parallel.For(
                0,
                n,
                new ParallelOptions {MaxDegreeOfParallelism = 4},
                i => { histogram.Report(i); });

            Scrape(histogram)
                .Should()
                .BeEquivalentTo(
                    new List<(double, double)>
                    {
                        (101, 100),
                        (100, 200),
                        (100, 300),
                        (699, double.PositiveInfinity)
                    });
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var aggregationParameters = new Dictionary<string, string>
            {
                {"a", "aa"},
                {"b", "bb"}
            };

            var histogram = context.CreateHistogram(
                "name",
                new HistogramConfig
                {
                    Unit = "unit",
                    AggregationParameters = aggregationParameters,
                    Buckets = new HistogramBuckets(10, 20, 30)
                });

            histogram.Report(42);
            histogram.Report(42);

            var timestamp = DateTimeOffset.Now;
            var metric = ((Histogram)histogram).Scrape(timestamp).Single(e => e.AggregationParameters.GetHistogramBucket().Value.UpperBound == double.PositiveInfinity);

            metric.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        2,
                        new MetricTags(new MetricTag(WellKnownTagKeys.Name, "name")),
                        timestamp,
                        "unit",
                        WellKnownAggregationTypes.Histogram,
                        aggregationParameters.SetHistogramBucket(new HistogramBucket(30, double.PositiveInfinity))
                    ));
        }

        [Test]
        public void Should_be_auto_scrapable()
        {
            var sum = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => { sum += e.Value; })));

            var histogram = (Histogram)context.CreateHistogram("name", new HistogramConfig {ScrapePeriod = 10.Milliseconds()});

            histogram.Report(1);
            histogram.Report(2);
            histogram.Report(3);
            Thread.Sleep(300.Milliseconds());

            sum.Should().Be(3);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var sum = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => { sum += e.Value; })));

            var histogram = (Histogram)context.CreateHistogram("name", new HistogramConfig {ScrapePeriod = 10.Milliseconds()});

            histogram.Dispose();

            Thread.Sleep(100.Milliseconds());
            histogram.Report(42);
            Thread.Sleep(300.Milliseconds());

            sum.Should().Be(0);
        }

        private static List<(double count, double upperBound)> Scrape(ITimer histogram, DateTimeOffset? timestamp = null)
        {
            return ((Histogram)histogram)
                .Scrape(timestamp ?? DateTimeOffset.Now)
                .Select(e => (e.Value, e.AggregationParameters.GetHistogramBucket().Value.UpperBound))
                .ToList();
        }
    }
}