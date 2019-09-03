using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Commons.Testing;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Tests.Models
{
    [TestFixture]
    internal class MetricDataPoint_Tests
    {
        private MetricDataPoint point;

        [Test]
        public void Should_not_accept_empty_tags_with_null_name()
        {
            Action action = () => point = new MetricDataPoint(1);

            action.Should().Throw<ArgumentException>().Which.ShouldBePrinted();
        }

        [Test]
        public void Should_not_accept_empty_tags_with_empty_name()
        {
            Action action = () => point = new MetricDataPoint(1, "");

            action.Should().Throw<ArgumentException>().Which.ShouldBePrinted();
        }

        [Test]
        public void Should_accept_empty_tags_when_provided_with_a_name()
        {
            Action action = () => point = new MetricDataPoint(1, "name");

            action.Should().NotThrow();
        }

        [Test]
        public void ToMetricEvent_should_preserve_point_timestamp()
        {
            point = new MetricDataPoint(1, "name") {Timestamp = DateTimeOffset.Now - 2.Hours()};

            point.ToMetricEvent(MetricTags.Empty).Timestamp.Should().Be(point.Timestamp.Value);
        }

        [Test]
        public void ToMetricEvent_should_provide_default_timestamp()
        {
            point = new MetricDataPoint(1, "name");

            point.ToMetricEvent(MetricTags.Empty).Timestamp.Should().BeCloseTo(DateTimeOffset.Now, 1.Minutes());
        }

        [Test]
        public void ToMetricEvent_should_merge_context_tags_and_point_tags()
        {
            var contextTags = MetricTags.Empty
                .Append("c1", "v1")
                .Append("c2", "v2");

            point = new MetricDataPoint(1, ("k1", "v1"), ("k2", "v2"));

            point.ToMetricEvent(contextTags)
                .Tags.Should()
                .Equal(
                    contextTags
                        .Append("k1", "v1")
                        .Append("k2", "v2"));
        }

        [Test]
        public void ToMetricEvent_should_merge_context_tags_name_and_point_tags()
        {
            var contextTags = MetricTags.Empty
                .Append("c1", "v1")
                .Append("c2", "v2");

            point = new MetricDataPoint(1, "metric", ("k1", "v1"), ("k2", "v2"));

            point.ToMetricEvent(contextTags)
                .Tags.Should()
                .Equal(
                    contextTags
                        .Append(WellKnownTagKeys.Name, "metric")
                        .Append("k1", "v1")
                        .Append("k2", "v2"));
        }

        [Test]
        public void ToMetricEvent_should_not_set_aggregation_type_or_parameters()
        {
            point = new MetricDataPoint(1, "name");

            point.ToMetricEvent(MetricTags.Empty).AggregationType.Should().BeNull();
            point.ToMetricEvent(MetricTags.Empty).AggregationParameters.Should().BeNull();
        }
    }
}