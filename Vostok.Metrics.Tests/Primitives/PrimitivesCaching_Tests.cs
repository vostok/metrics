using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Primitives.Counter;
using Vostok.Metrics.Primitives.Gauge;
using Vostok.Metrics.Primitives.Timer;

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
            context1 = new DevNullMetricContext();
            context2 = new DevNullMetricContext();
        }

        [Test]
        public void Should_not_cache_timers()
        {
            var timer1 = context1.CreateTimer("timer");
            var timer2 = context1.CreateTimer("timer");

            timer2.Should().NotBeSameAs(timer1);
        }

        [Test]
        public void Should_cache_func_gauges()
        {
            var gauge1 = context1.CreateFuncGauge("gauge", () => 1);
            var gauge2 = context1.CreateFuncGauge("gauge", () => 2);

            gauge2.Should().BeSameAs(gauge1);
        }

        [Test]
        public void Should_create_new_instance_after_dispose()
        {
            var gauge1 = context1.CreateFuncGauge("gauge", () => 1);
            gauge1.Dispose();
            var gauge2 = context1.CreateFuncGauge("gauge", () => 2);

            gauge2.Should().NotBeSameAs(gauge1);
        }

        [Test]
        public void Should_create_new_instance_after_dispose_for_metric_group()
        {
            var group1 = context1.CreateFuncGauge("gauge", "key1");
            var gauge1 = group1.For("value1");
            var gauge2 = group1.For("value1");

            gauge2.Should().BeSameAs(gauge1);

            group1.Dispose();

            var gauge3 = group1.For("value1");

            gauge3.Should().NotBeSameAs(group1);
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
        public void Should_not_cache_instances_between_different_contexts()
        {
            var counter1 = context1.CreateCounter("counter");
            var counter2 = context2.CreateCounter("counter");

            counter2.Should().NotBeSameAs(counter1);
        }

        [Test]
        public void Should_not_cache_instances_between_context_and_its_wrapper()
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
    }
}