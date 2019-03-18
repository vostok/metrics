using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Metrics.Model;
using Vostok.Metrics.Primitives.GaugeImpl;
using Vostok.Metrics.Primitives.TimingImpl;

namespace Vostok.Metrics.Tests
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
                    new GaugeConfig
                    {
                        ScrapePeriod = 10.Seconds()
                    });

            var gauge2 = rootContext.Gauge("nag", new GaugeConfig {ScrapePeriod = 10.Seconds()});
            gauge2.Inc();
        }

        [Test]
        public void Store_prepared_metric()
        {
            // todo MetricEvent builder
            // need this to send custom metrics to storage
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