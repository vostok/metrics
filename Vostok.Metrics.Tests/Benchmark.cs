using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NUnit.Framework;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Tests
{
    [TestFixture]
    [Explicit]
    public class ImmutableArrayDictionary_Benchmarks
    {
        [Test]
        public void ImmutableArrayDictionary_SettersBenchmark_Small_Preallocate() => BenchmarkRunner.Run<ImmutableArrayDictionary_SettersBenchmark>();
    }

    [MemoryDiagnoser]
    public abstract class ImmutableArrayDictionary_SettersBenchmark
    {
        private MetricGroup<SimpleCounter> g1;
        private MetricGroup<SimpleCounter> g2;
        private MetricGroup<SimpleCounter> g5;

        [GlobalSetup]
        public void Setup()
        {
            g1 = new MetricGroup<SimpleCounter>(tags => new SimpleCounter(tags), "key1");
            g2 = new MetricGroup<SimpleCounter>(tags => new SimpleCounter(tags), "key1", "key2");
            g5 = new MetricGroup<SimpleCounter>(tags => new SimpleCounter(tags), "key1", "key2", "key3", "key4", "key5");
        }

        [Benchmark]
        public object MetricGroupStructOneKey()
        {
            return g1.For("EE");
        }

        [Benchmark]
        public object MetricGroupStructTwoKeys()
        {
            return g2.For("A", "v");
        }

        [Benchmark]
        public object MetricGroupStructFiveKeys()
        {
            return g5.For("A", "v", "124", "rweherkjgh34uihg", "gggg");
        }

        private class SimpleCounter
        {
            public readonly ReadonlyInternalMetricTags Tags;
            public int Value;

            public SimpleCounter(ReadonlyInternalMetricTags tags)
            {
                Tags = tags;
            }
        }
    }
}
