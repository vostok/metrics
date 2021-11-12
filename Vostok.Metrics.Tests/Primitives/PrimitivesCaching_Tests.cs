using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Counter;
using Vostok.Metrics.Primitives.Gauge;
using Vostok.Metrics.Primitives.Timer;
using Vostok.Metrics.Senders;

namespace Vostok.Metrics.Tests.Primitives
{
    [TestFixture]
    internal class PrimitivesCaching_Tests
    {
        private IMetricContext context1;
        private IMetricContext context2;

        [SetUp]
        public void TestSetup()
        {
            context1 = new MetricContext(new MetricContextConfig(new DevNullMetricEventSender()));
            context2 = new MetricContext(new MetricContextConfig(new DevNullMetricEventSender()));
        }

        [Test]
        public void Should_not_cache_for_dev_null_context()
        {
            var context = new DevNullMetricContext();
            
            var c1 = context.CreateCounter("c");
            var c2 = context.CreateCounter("c");

            c2.Should().NotBeSameAs(c1);
        }
        
        [Test]
        public void Should_clean_cache_on_context_dispose()
        {
            var context = new MetricContext(new MetricContextConfig(new DevNullMetricEventSender()));
            
            var c1 = context.CreateCounter("c");
            var c2 = context.CreateCounter("c");

            c2.Should().BeSameAs(c1);
            
            context.Dispose();
            
            var c3 = context.CreateCounter("c");
            
            c3.Should().NotBeSameAs(c1);
        }
        
        [Test]
        public void Should_not_cache_timers()
        {
            var timer1 = context1.CreateTimer("timer");
            var timer2 = context1.CreateTimer("timer");

            timer2.Should().NotBeSameAs(timer1);
        }

        [Test]
        public void Should_not_cache_func_gauges()
        {
            var gauge1 = context1.CreateFuncGauge("gauge", () => 1);
            var gauge2 = context1.CreateFuncGauge("gauge", () => 2);

            gauge2.Should().NotBeSameAs(gauge1);
        }

        [Test]
        public void Should_not_cache_multi_func_gauges()
        {
            var gauge1 = context1.CreateMultiFuncGauge(() => new[] {new MetricDataPoint(1, "metric1")});
            var gauge2 = context1.CreateMultiFuncGauge(() => new[] {new MetricDataPoint(1, "metric1")});

            gauge2.Should().NotBeSameAs(gauge1);
        }

        [Test]
        public void Should_cache_counters()
        {
            var counter1 = context1.CreateCounter("counter");
            var counter2 = context1.CreateCounter("counter");

            counter2.Should().BeSameAs(counter1);
        }

        [Test]
        public void Should_cache_histograms()
        {
            var histogram1 = context1.CreateHistogram("histogram");
            var histogram2 = context1.CreateHistogram("histogram");

            histogram2.Should().BeSameAs(histogram1);
        }

        [Test]
        public void Should_cache_summaries()
        {
            var summary1 = context1.CreateSummary("summary");
            var summary2 = context1.CreateSummary("summary");

            summary2.Should().BeSameAs(summary1);
        }

        [Test]
        public void Should_cache_integer_gauges()
        {
            var gauge1 = context1.CreateIntegerGauge("gauge");
            var gauge2 = context1.CreateIntegerGauge("gauge");

            gauge2.Should().BeSameAs(gauge1);
        }

        [Test]
        public void Should_cache_floating_gauges()
        {
            var gauge1 = context1.CreateFloatingGauge("gauge");
            var gauge2 = context1.CreateFloatingGauge("gauge");

            gauge2.Should().BeSameAs(gauge1);
        }

        [Test]
        public void Should_not_cache_instances_between_contexts_with_different_tags()
        {
            context2 = context1.WithTag("k", "v");

            var counter1 = context1.CreateCounter("counter");
            var counter2 = context2.CreateCounter("counter");

            counter2.Should().NotBeSameAs(counter1);
        }

        [Test]
        public void Should_not_cache_instances_between_different_names()
        {
            var counter1 = context1.CreateCounter("counter1");
            var counter2 = context1.CreateCounter("counter2");

            counter2.Should().NotBeSameAs(counter1);
        }

        [Test]
        public void Should_not_cache_instances_between_different_types()
        {
            var counter1 = context1.CreateCounter("primitive");
            var counter2 = context1.CreateHistogram("primitive");

            counter2.Should().NotBeSameAs(counter1);
        }

        [Test]
        public void Should_cache_metric_groups_correctly()
        {
            var group1 = context1.CreateCounter("primitive", "t1");
            var group2 = context1.CreateCounter("primitive", "t1");
            var group3 = context1.CreateCounter("primitive", "t2");

            group2.Should().BeSameAs(group1);
            group3.Should().NotBeSameAs(group1);
        }

        [Test]
        public void Should_cache_for_metric_groups_correctly()
        {
            var group1 = context1.CreateCounter("primitive", "t1");
            var group2 = context1.CreateCounter("primitive", "t2");

            var counter1 = group1.For("c1");
            var counter2 = group1.For("c1");
            var counter3 = group2.For("c1");

            counter2.Should().BeSameAs(counter1);
            counter3.Should().NotBeSameAs(counter1);
        }

        [Test]
        public void Should_cache_between_different_contexts_with_same_tags()
        {
            var counter1 = context1.WithTag("a", "b").CreateCounter("counter");
            var counter2 = context1.WithTag("a", "b").CreateCounter("counter");

            counter2.Should().BeSameAs(counter1);
        }

        [Test]
        public void Should_cache_between_different_contexts_with_same_multiple_tags()
        {
            var counter1 = context1.WithTags(new MetricTags(new MetricTag("a1", "b1"), new MetricTag("a2", "b2"))).CreateCounter("counter");
            var counter2 = context1.WithTags(new MetricTags(new MetricTag("a1", "b1"), new MetricTag("a2", "b2"))).CreateCounter("counter");

            counter2.Should().BeSameAs(counter1);
        }

        [Test]
        public void Should_cache_between_different_contexts_with_same_multiple_tags_structure()
        {
            var counter1 = context1.WithTags(new MetricTags(new MetricTag("a1", "b1"), new MetricTag("a2", "b2"))).CreateCounter("counter");
            var counter2 = context1.WithTag("a1", "b1").WithTag("a2", "b2").CreateCounter("counter");

            counter2.Should().BeSameAs(counter1);
        }
    }
}