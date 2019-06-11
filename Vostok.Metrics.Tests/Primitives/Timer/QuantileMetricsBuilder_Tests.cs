using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Timer;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class QuantileMetricsBuilder_Tests
    {
        [Test]
        public void Should_build_quantiles()
        {
            var values = Enumerable.Range(0, 100).Select(i => (i + 42) % 100).Select(i => (double) i).ToArray();
            var metrics = new QuantileMetricsBuilder(new[] {0, 0.33, 0.73, 1}, MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(6);

            Get(metrics, WellKnownTagValues.AggregateAverage).Should().Be(49.5);
            Get(metrics, WellKnownTagValues.AggregateCount).Should().Be(100);

            Get(metrics, "p0").Should().Be(0);
            Get(metrics, "p33").Should().Be(33);
            Get(metrics, "p73").Should().Be(73);
            Get(metrics, "p100").Should().Be(99);
        }

        [Test]
        public void Should_use_default_quantiles_if_null_specified()
        {
            var values = Enumerable.Range(0, 100).Select(i => (i + 42) % 100).Select(i => (double)i).ToArray();
            var metrics = new QuantileMetricsBuilder(null, MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(Quantiles.DefaultQuantiles.Length + 2);
        }

        [Test]
        public void Should_build_avg_count_without_quantiles()
        {
            var values = Enumerable.Range(0, 100).Select(i => (i + 42) % 100).Select(i => (double)i).ToArray();
            var metrics = new QuantileMetricsBuilder(new double[0], MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(2);

            Get(metrics, WellKnownTagValues.AggregateAverage).Should().Be(49.5);
            Get(metrics, WellKnownTagValues.AggregateCount).Should().Be(100);
        }

        [Test]
        public void Should_build_min_max_with_0_1_quantiles()
        {
            var values = Enumerable.Range(0, 100).Select(i => (i + 42) % 100).Select(i => (double)i).ToArray();
            var metrics = new QuantileMetricsBuilder(new double[] {0, 1}, MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(4);

            Get(metrics, WellKnownTagValues.AggregateAverage).Should().Be(49.5);
            Get(metrics, WellKnownTagValues.AggregateCount).Should().Be(100);

            Get(metrics, "p0").Should().Be(0);
            Get(metrics, "p100").Should().Be(99);
        }

        [Test]
        public void Should_return_zeros_without_values()
        {
            var values = new double[0];
            var metrics = new QuantileMetricsBuilder(new[] { 0, 0.33, 0.73, 1 }, MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(6);

            Get(metrics, WellKnownTagValues.AggregateAverage).Should().Be(0);
            Get(metrics, WellKnownTagValues.AggregateCount).Should().Be(0);

            Get(metrics, "p0").Should().Be(0);
            Get(metrics, "p33").Should().Be(0);
            Get(metrics, "p73").Should().Be(0);
            Get(metrics, "p100").Should().Be(0);
        }

        [Test]
        public void Should_use_only_prefix_of_values_if_size_specified()
        {
            var values = Enumerable.Range(0, 100).Select(i => (i + 42) % 100)
                .Concat(Enumerable.Repeat(999, 100))
                .Select(i => (double)i)
                .ToArray();
            var metrics = new QuantileMetricsBuilder(new[] { 0, 0.33, 0.73, 1 }, MetricTags.Empty, "unit")
                .Build(values, 100, 999, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(6);

            Get(metrics, WellKnownTagValues.AggregateAverage).Should().Be(49.5);
            Get(metrics, WellKnownTagValues.AggregateCount).Should().Be(999);

            Get(metrics, "p0").Should().Be(0);
            Get(metrics, "p33").Should().Be(33);
            Get(metrics, "p73").Should().Be(73);
            Get(metrics, "p100").Should().Be(99);
        }

        [Test]
        public void Should_throw_if_bad_quantile_provided()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new QuantileMetricsBuilder(new double[] {42}, MetricTags.Empty, "unit");
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        private static double Get(IEnumerable<MetricEvent> metrics, string tag)
        {
            var metric = metrics.Single(m => m.Tags.Any(t => t.Value == tag));
            return metric.Value;
        }
    }
}