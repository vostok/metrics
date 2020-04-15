using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Models;
using Vostok.Metrics.Primitives.Timer;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class Quantiles_Tests
    {
        [Test]
        public void GetQuantile_should_works_with_sorted_values()
        {
            Quantiles.GetQuantile(0, new double[] {1, 2, 3}, 3).Should().Be(1);
            Quantiles.GetQuantile(0.5, new double[] {1, 2, 3}, 3).Should().Be(2);
            Quantiles.GetQuantile(1, new double[] {1, 2, 3}, 3).Should().Be(3);
            Quantiles.GetQuantile(0.5, new double[] {1, 2, 3, 4}, 4).Should().Be(3);
            Quantiles.GetQuantile(0.3, new double[] {1, 2, 3}, 3).Should().Be(1);
        }

        [Test]
        public void QuantileTags_should_works_for_integer_percentiles()
        {
            for (var p = 0; p <= 100; p++)
            {
                var result = Quantiles.QuantileTags(new[] {p / 100.0}, MetricTags.Empty).Single().First().Value;
                result.Should().Be($"p{p}");
            }
        }

        [TestCase(0.09, "p9")]
        [TestCase(0.9, "p90")]
        [TestCase(0.99, "p99")]
        [TestCase(0.999, "p999")]
        [TestCase(0.9999, "p9999")]
        [TestCase(0.99999, "p99999")]
        [TestCase(0.999999, "p999999")]
        public void QuantileTags_should_works_for_double_percentiles(double quantile, string expected)
        {
            Quantiles.QuantileTag(quantile).Value.Should().Be(expected);
        }
    }
}