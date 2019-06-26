using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Primitives.Timer;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class HistogramBucket_Tests
    {
        [Test]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void Should_throw_on_incorrect_bounds()
        {
            new Action(() => new HistogramBucket(1, 2)).Should().NotThrow();
            new Action(() => new HistogramBucket(2, 2)).Should().Throw<ArgumentException>();
            new Action(() => new HistogramBucket(2, 1)).Should().Throw<ArgumentException>();
        }
    }
}