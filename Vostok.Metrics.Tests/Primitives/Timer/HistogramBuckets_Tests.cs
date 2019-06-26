using System;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Primitives.Timer;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class HistogramBuckets_Tests
    {
        [Test]
        public void Should_throw_on_non_increasing_upper_bounds()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new HistogramBuckets(1, 2, 3, 2, 4, 5);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_throw_on_empty_upper_bounds()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new HistogramBuckets();
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreateLinear_should_works_correctly()
        {
            var buckets = HistogramBuckets.CreateLinear(10, 3, 4);
            buckets.Count.Should().Be(5);
            buckets[0].Should().BeEquivalentTo(new HistogramBucket(double.NegativeInfinity, 10));
            buckets[1].Should().BeEquivalentTo(new HistogramBucket(10, 13));
            buckets[2].Should().BeEquivalentTo(new HistogramBucket(13, 16));
            buckets[3].Should().BeEquivalentTo(new HistogramBucket(16, 19));
            buckets[4].Should().BeEquivalentTo(new HistogramBucket(19, double.PositiveInfinity));
        }

        [Test]
        public void CreateExponential_should_works_correctly()
        {
            var buckets = HistogramBuckets.CreateExponential(10, 3, 4);
            buckets.Count.Should().Be(5);
            buckets[0].Should().BeEquivalentTo(new HistogramBucket(double.NegativeInfinity, 10));
            buckets[1].Should().BeEquivalentTo(new HistogramBucket(10, 10 * 3));
            buckets[2].Should().BeEquivalentTo(new HistogramBucket(10 * 3, 10 * 9));
            buckets[3].Should().BeEquivalentTo(new HistogramBucket(10 * 9, 10 * 27));
            buckets[4].Should().BeEquivalentTo(new HistogramBucket(10 * 27, double.PositiveInfinity));
        }

        [TestCase(-1000, 0)]
        [TestCase(-11, 0)]
        [TestCase(-10, 0)]
        [TestCase(-9, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(29, 2)]
        [TestCase(30, 2)]
        [TestCase(31, 3)]
        [TestCase(1000, 3)]
        public void FindBucketIndex_should_works_correctly(double value, int index)
        {
            var buckets = new HistogramBuckets(-10, 5, 30);
            buckets.Count.Should().Be(4);

            buckets[0].Should().BeEquivalentTo(new HistogramBucket(double.NegativeInfinity, -10));
            buckets[1].Should().BeEquivalentTo(new HistogramBucket(-10, 5));
            buckets[2].Should().BeEquivalentTo(new HistogramBucket(5, 30));
            buckets[3].Should().BeEquivalentTo(new HistogramBucket(30, double.PositiveInfinity));

            buckets.FindBucketIndex(value).Should().Be(index);
        }
    }
}