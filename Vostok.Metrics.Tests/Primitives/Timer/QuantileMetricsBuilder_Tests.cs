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

            metrics.Count.Should().Be(8);

            Get(metrics, WellKnownTagValues.AggregateMin).Should().Be(0);
            Get(metrics, WellKnownTagValues.AggregateMax).Should().Be(99);
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

            metrics.Count.Should().Be(Quantiles.DefaultQuantiles.Length + 4);
        }

        [Test]
        public void Should_build_min_max_avg_without_quantiles()
        {
            var values = Enumerable.Range(0, 100).Select(i => (i + 42) % 100).Select(i => (double)i).ToArray();
            var metrics = new QuantileMetricsBuilder(new double[0], MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(4);

            Get(metrics, WellKnownTagValues.AggregateMin).Should().Be(0);
            Get(metrics, WellKnownTagValues.AggregateMax).Should().Be(99);
            Get(metrics, WellKnownTagValues.AggregateAverage).Should().Be(49.5);
            Get(metrics, WellKnownTagValues.AggregateCount).Should().Be(100);
        }

        [Test]
        public void Should_return_zeros_without_values()
        {
            var values = new double[0];
            var metrics = new QuantileMetricsBuilder(new[] { 0, 0.33, 0.73, 1 }, MetricTags.Empty, "unit").Build(values, DateTimeOffset.Now).ToList();

            metrics.Count.Should().Be(8);

            Get(metrics, WellKnownTagValues.AggregateMin).Should().Be(0);
            Get(metrics, WellKnownTagValues.AggregateMax).Should().Be(0);
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

            metrics.Count.Should().Be(8);

            Get(metrics, WellKnownTagValues.AggregateMin).Should().Be(0);
            Get(metrics, WellKnownTagValues.AggregateMax).Should().Be(99);
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

        [Test]
        public void SetUnit_should_update_unit()
        {
            var values = new double[] {1, 2, 3};
            var builder = new QuantileMetricsBuilder(new[] { 0, 0.33, 0.73, 1 }, MetricTags.Empty, "unit1");

            var metrics = builder.Build(values, DateTimeOffset.Now).ToList();
            metrics.All(m => m.Unit == "unit1" || m.Tags.Any(t => t.Value == WellKnownTagValues.AggregateCount)).Should().BeTrue();

            builder.SetUnit("unit2");
            metrics = builder.Build(values, DateTimeOffset.Now).ToList();
            metrics.All(m => m.Unit == "unit2" || m.Tags.Any(t => t.Value == WellKnownTagValues.AggregateCount)).Should().BeTrue();
        }

        [Test]
        public void SetQuantiles_should_update_quantiles()
        {
            var values = new double[] { 1, 2, 3 };
            var builder = new QuantileMetricsBuilder(new double[0], MetricTags.Empty, "unit1");

            var metrics = builder.Build(values, DateTimeOffset.Now).ToList();
            metrics.Count.Should().Be(4);

            builder.SetQuantiles(new[] {0.5});
            metrics = builder.Build(values, DateTimeOffset.Now).ToList();
            metrics.Count.Should().Be(5);
            Get(metrics, "p50").Should().Be(2);
        }

        private static double Get(IEnumerable<MetricEvent> metrics, string tag)
        {
            var metric = metrics.Single(m => m.Tags.Any(t => t.Value == tag));
            return metric.Value;
        }
    }
}