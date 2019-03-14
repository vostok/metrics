using System;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions.Extensions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using Vostok.Metrics.Abstractions.DynamicTags.StringKeys;
using Vostok.Metrics.Abstractions.MoveToImplementation.GaugeImpl;
using Vostok.Metrics.Abstractions.MoveToImplementation.HistogramImpl;
using Vostok.Metrics.Abstractions.MoveToImplementation.TimingImpl;

namespace Vostok.Metrics.Abstractions.Tests
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

        private class SignalServiceMetrics : IDisposable
        {
            // graphite path:
            // env-Infra.service-Loki.cluster-Loki-default.Replica-1.
            // signals-requests-count.
            // req-{create,set,count}.clid-{fat-service,...}.result-{NotFound,Set,...}
            private readonly ITaggedMetric3<IGauge> requestCounter;
            
            private readonly ITaggedMetric1<IGauge> expiredRequestsCounter;
            
            //design We should have separate fields for them, its so ugly
            private readonly IDisposable reqCounterRegistration;
            private readonly IDisposable signalsExpiredRegistration;

            public SignalServiceMetrics(IMetricContext context, TimeSpan scrapePeriod)
            {
                requestCounter = context.Gauge(
                    "signals-requests-count",
                    "req", "clid", "result",
                    out reqCounterRegistration,
                    scrapePeriod);
                expiredRequestsCounter = context.Gauge(
                    "signals-expired-signals",
                    "state",
                    out signalsExpiredRegistration,
                    scrapePeriod);
            }

            public void ReportCreate(string clid, SignalCreateResult result)
            {
                //design Why should I always write ToString() here. Stupid library devs =.=
                GetRequestCounter("create", clid, result.ToString())
                    .Inc();
            }

            public void ReportSet(string clid, TerminalSignalState result)
            {
                GetRequestCounter("set", clid, result.ToString())
                    .Inc();
            }

            public void ReportWait(string clid, SignalServiceWaitResult waitResult)
            {
                GetRequestCounter("wait", clid, waitResult.ToString())
                    .Inc();
            }

            public void ReportExpiredItem(TerminalSignalState state)
            {
                expiredRequestsCounter
                    .For(state.ToString())
                    .Inc();
            }

            private IGauge GetRequestCounter(string request, string clid, string requestResult)
            {
                return requestCounter.For(request, clid, requestResult);
            }

            public void Dispose()
            {
                reqCounterRegistration.Dispose();
                signalsExpiredRegistration.Dispose();
            }
        }
    }

    public class LockNamespaceMetrics : IDisposable
    {
        private readonly ITiming lockTime;
        
        public LockNamespaceMetrics(string namespaceName, IMetricContext clusterContext, IMetricContext replicaContext)
        {
            lockTime = clusterContext
                .WithTag("lock-namespace", namespaceName)
                //design It's hard to discover MetricUnits class
                .Timing("lock-time", new TimingConfig{Unit = MetricUnits.Seconds});
        }

        public void RecordLockTime(TimeSpan time)
        {
            //design Why should I care about seconds/milliseconds here?
            lockTime.Report(time.TotalSeconds);
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