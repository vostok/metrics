using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Gauge;
using Vostok.Metrics.Senders;

// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Vostok.Metrics.Tests.Primitives.Gauge
{
    [TestFixture]
    internal class ListFuncGauge_Tests
    {
        private MetricContext context;
        private List<object> values;
        private Func<List<object>> func;

        [SetUp]
        public void SetUp()
        {
            context = new MetricContext(
                new MetricContextConfig(new DevNullMetricEventSender())
                {
                    DefaultScrapePeriod = TimeSpan.MaxValue,
                    Tags = MetricTags.Empty
                        .Append("context-tag-key-1", "context-tag-value-1")
                        .Append("context-tag-key-2", "context-tag-value-2")
                });

            func = () => values;
        }

        [Test]
        public void Should_scrape_func_values()
        {
            var gauge = context.CreateListFuncGauge("name", func);

            values = new List<object> {new Model1(1, "x", 100), new Model1(2, "y", 200)};
            Scrape(gauge).Select(e => e.Value).Should().BeEquivalentTo(100, 200);

            values = new List<object> {new Model2(300), new Model1(1, "x", 400), new Model1(2, "y", 500)};
            Scrape(gauge).Select(e => e.Value).Should().BeEquivalentTo(300, 400, 500);
        }

        [Test]
        public void Should_scrape_empty_values()
        {
            var gauge = context.CreateListFuncGauge("name", func);
            values = new List<object>();
            Scrape(gauge).Should().BeEmpty();
        }

        [Test]
        public void Should_scrape_only_valid_func_values()
        {
            var x = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, e.Value))) {ErrorCallback = Console.WriteLine});

            context.CreateListFuncGauge("name", func, new ListFuncGaugeConfig {ScrapePeriod = 10.Milliseconds()});

            values = new List<object> {new BadModel(), new Model2(200)};

            Thread.Sleep(300.Milliseconds());

            x.Should().Be(200);
        }

        [Test]
        public void Should_fill_metric_event()
        {
            var gauge = context.CreateListFuncGauge(
                "name",
                func,
                new ListFuncGaugeConfig
                {
                    ScrapePeriod = TimeSpan.MaxValue,
                    Unit = "unit"
                });

            values = new List<object> {new Model1(1, "x", 100), new Model2(200)};

            var timestamp = DateTimeOffset.Now;
            var metric = Scrape(gauge, timestamp);

            metric.Should()
                .BeEquivalentTo(
                    new MetricEvent(
                        100,
                        context.Tags.Append(WellKnownTagKeys.Name, "name").Append("Prop1", "1").Append("Prop2", "x"),
                        timestamp,
                        "unit",
                        null,
                        null
                    ),
                    new MetricEvent(
                        200,
                        context.Tags.Append(WellKnownTagKeys.Name, "name"),
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

            context.CreateListFuncGauge("name", func, new ListFuncGaugeConfig {ScrapePeriod = 10.Milliseconds()});

            values = new List<object> {new Model2(1)};

            Thread.Sleep(300.Milliseconds());

            x.Should().Be(1);
        }

        [Test]
        public void Should_not_be_scraped_after_dispose()
        {
            var x = 0.0;
            context = new MetricContext(new MetricContextConfig(new AdHocMetricEventSender(e => Interlocked.Exchange(ref x, e.Value))));

            values = new List<object> {new Model2(0)};
            var gauge = context.CreateListFuncGauge("name", func, new ListFuncGaugeConfig {ScrapePeriod = 10.Milliseconds()});
            gauge.Dispose();

            Thread.Sleep(100.Milliseconds());
            values = new List<object> {new Model2(2)};
            Thread.Sleep(300.Milliseconds());

            x.Should().Be(0);
        }

        private static IEnumerable<MetricEvent> Scrape(IListFuncGauge gauge, DateTimeOffset? timestamp = null)
        {
            return ((ListFuncGauge)gauge).Scrape(timestamp ?? DateTimeOffset.Now);
        }

        private class Model1
        {
            public Model1(int prop1, string prop2, double value)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Value = value;
            }

            [MetricTag(1)]
            public int Prop1 { get; }

            [MetricTag(2)]
            public string Prop2 { get; }

            [MetricValue]
            public double Value { get; }
        }

        private class Model2
        {
            public Model2(double value)
            {
                Value = value;
            }

            [MetricValue]
            public double Value { get; }
        }

        private class BadModel
        {
        }
    }
}