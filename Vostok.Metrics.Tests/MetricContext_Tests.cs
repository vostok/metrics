using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Commons.Testing;
using Vostok.Commons.Threading;
using Vostok.Metrics.Models;
using Vostok.Metrics.Scraping;

namespace Vostok.Metrics.Tests
{
    [TestFixture]
    internal class MetricContext_Tests
    {
        private MetricContext context;

        [SetUp]
        public void TestSetup()
            => context = new MetricContext(new MetricContextConfig(Substitute.For<IMetricEventSender>()));

        [Test]
        public void Should_implement_annotation_context()
            => context.AsAnnotationContext().Should().NotBeNull();

        [Test]
        public void Should_implement_annotation_context_if_enriched_with_tags()
            => context.WithTag("key", "value").AsAnnotationContext().Should().NotBeNull();

        [Test]
        public void Should_not_scrape_after_dispose()
        {
            var metric = new MyScrapableClass();
            var registration = context.Register(metric, 0.01.Seconds());
            Thread.Sleep(3.Seconds());
            
            var task = Task.Run(() => registration.Dispose());
            task.ShouldNotCompleteIn(3.Seconds());
            metric.Signal.Set();
            
            task.ShouldCompleteIn(3.Seconds());
        }

        private class MyScrapableClass : IScrapableMetric
        {
            public readonly AsyncManualResetEvent Signal = new AsyncManualResetEvent(false);
            
            public IEnumerable<MetricEvent> Scrape(DateTimeOffset timestamp)
            {
                Signal.GetAwaiter().GetResult();
                yield break;
            }
        }
    }
}
