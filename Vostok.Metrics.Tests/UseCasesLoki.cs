using System;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Primitives.GaugePrimitive;
using Vostok.Metrics.Primitives.TimerPrimitive;
using Vostok.Metrics.Primitives.TimerPrimitive.TimerImpl;

namespace Vostok.Metrics.Tests
{
    [TestFixture]
    [Ignore("Delete these tests when design is completed")]
    public class UseCasesLoki
    {
        [Test]
        public void Loki_scenario()
        {
            var rootContext = Substitute.For<IMetricContext>(); // get this from houston
            var clusterContext = rootContext // environment tag already specified
                .WithTag("service", "Loki")
                .WithTag("cluster", "Loki-default");
            var replicaContext = clusterContext
                .WithTag("replica", "{1}"); // pass replica number instead of {1}
            
            // somewhere in CreateLokiState method
            var signalMetrics = new SignalServiceMetrics(replicaContext, 10.Seconds());
            var locksMetrics = new LockNamespaceMetrics("Namespace-name", clusterContext, replicaContext);
            
            // somewhere in SignalService or LockService code
            signalMetrics.ReportCreate("fat-service", SignalCreateResult.Created);
            locksMetrics.RecordLockTime(1.Seconds());
            
            // when you drop state
            signalMetrics.Dispose();
            locksMetrics.Dispose();
        }

        
        // todo add this to guidelines
        private class SignalServiceMetrics : IDisposable
        {
            // graphite path:
            // env-Infra.service-Loki.cluster-Loki-default.Replica-1.
            // signals-requests-count.
            // req-{create,set,count}.clid-{fat-service,...}.result-{NotFound,Set,...}
            private readonly ITaggedMetric3<IGauge> requestCounter;
            private readonly ITaggedMetric1<IGauge> expiredRequestsCounter;

            public SignalServiceMetrics(IMetricContext context, TimeSpan scrapePeriod)
            {
                requestCounter = context.Gauge(
                    "signals-requests-count",
                    "req", "clid", "result",
                    new GaugeConfig{ScrapePeriod = scrapePeriod});
                expiredRequestsCounter = context.Gauge(
                    "signals-expired-signals",
                    "state",
                    new GaugeConfig{ScrapePeriod = scrapePeriod});
            }

            public void ReportCreate(string clid, SignalCreateResult result)
            {
                requestCounter.For("create", clid, result).Inc();
            }

            public void ReportSet(string clid, TerminalSignalState result)
            {
                requestCounter.For("set", clid, result).Inc();
            }

            public void ReportWait(string clid, SignalServiceWaitResult waitResult)
            {
                requestCounter.For("wait", clid, waitResult).Inc();
            }

            public void ReportExpiredItem(TerminalSignalState state)
            {
                expiredRequestsCounter.For(state).Inc();
            }

            public void Dispose()
            {
                requestCounter.Dispose();
                expiredRequestsCounter.Dispose();
            }
        }
    }

    public class LockNamespaceMetrics : IDisposable
    {
        private readonly ITimer lockTime;
        
        public LockNamespaceMetrics(string namespaceName, IMetricContext clusterContext, IMetricContext replicaContext)
        {
            lockTime = clusterContext
                .WithTag("lock-namespace", namespaceName)
                .Timer("lock-time", new TimerConfig{Unit = WellKnownUnits.Time.Seconds});
        }

        public void RecordLockTime(TimeSpan time)
        {
            lockTime.Report(time);
        }

        public void Dispose()
        {
        }
    }

    internal enum SignalServiceWaitResult
    {
        NotFound,
        Timeout,
        Set,
        Expired
    }
    internal enum SignalCreateResult
    {
        Created = 0,
        AlreadyExists = 1
    }
    
    internal enum TerminalSignalState
    {
        Set,
        Expired
    }
}