using System;
using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Metrics.Model;
using Vostok.Metrics.Primitives.Gauge;
using Vostok.Metrics.Primitives.Timer;

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
                .CreateFuncGauge("queue-length",
                    () => queue.Count,
                    new FuncGaugeConfig
                    {
                        ScrapePeriod = 10.Seconds()
                    });

            var gauge2 = rootContext.CreateIntegerGauge("nag", new IntegerGaugeConfig {ScrapePeriod = 10.Seconds()});
            gauge2.Increment();
        }

        [Test]
        public void Store_prepared_metric()
        {
            // todo MetricSample builder
            // need this to send custom metrics to storage
            rootContext
                .Send(new MetricEvent(
                    10,
                    MetricTagsMerger.Merge(rootContext.Tags, "my-custom-metric"),
                    DateTimeOffset.Now,
                    WellKnownUnits.Seconds,
                    null,
                    null));
        }

        [Test]
        public void Dynamic_tags_string_keys()
        {
            var latenciesClidUrl = rootContext
                .CreateTimer("requests-latency", "clid", "url");
            var latenciesGlobal = rootContext
                .CreateTimer("requests-latency");

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