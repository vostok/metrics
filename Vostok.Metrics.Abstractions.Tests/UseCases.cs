using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Metrics.Abstractions.Model;
using Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl;
using Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl;

namespace Vostok.Metrics.Abstractions.Tests
{
    [TestFixture]
    [Ignore("Abstractions are not implemented yet")]
    public class UseCases
    {
        private IMetricContext rootContext;
        
        [SetUp]
        public void SetUp()
        {
            rootContext = Substitute.For<IMetricContext>();
        }
        
        [Test]
        public void Whitebox_gauge_metric()
        {
            var queue = new Queue<int>();
            
            rootContext
                .Gauge("queue-length",
                    () => queue.Count,
                    out _,
                    10.Seconds(),
                    GaugeConfig.Default);

            var gauge2 = rootContext.Gauge("nag", out var _, 10.Seconds());
        }

        [Test]
        public void Store_prepared_metric()
        {
            // todo MEtricEvent builder
            rootContext
                .Send(new MetricEvent(
                    10,
                    DateTimeOffset.Now,
                    MetricUnits.Seconds,
                    null,
                    MetricTagsMerger.Merge(rootContext.Tags, "my-custom-metric")));
        }

        [Test]
        public void Dynamic_tags_string_keys()
        {
            var latenciesClidUrl = rootContext
                .Timing("requests-latency", "clid", "url");
            var latenciesGlobal = rootContext
                .Timing("requests-latency");

            // Get these values from http request
            var clid = "fat-service";
            var url = "GET /smth";
            
            var sw = Stopwatch.StartNew();
            // ... your business code here ...
            var elapsed = sw.Elapsed;
            
            latenciesClidUrl.For(clid, url).Report(elapsed.TotalSeconds);
            latenciesGlobal.Report(elapsed.TotalSeconds);
        }
    }
}