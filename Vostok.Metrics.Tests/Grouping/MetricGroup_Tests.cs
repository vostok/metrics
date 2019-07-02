using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Tests.Grouping
{
    [TestFixture]
    internal class MetricGroup_Tests
    {
        [Test]
        public void Should_use_fabric_for_create_new_or_return_existing()
        {
            var group = new MetricGroup<SimpleCounter>(tags => new SimpleCounter(tags), "key1", "key2");

            group.For("a", "b1").Value = 1;
            group.For("a", "b2").Value = 2;

            group.For("a", "b1").Value.Should().Be(1);
            group.For("a", "b2").Value.Should().Be(2);

            group.For("a", "b1")
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("key1", "a").Append("key2", "b1"));
            group.For("a", "b2")
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("key1", "a").Append("key2", "b2"));
        }

        [Test]
        public void Should_create_new_for_different_dynamic_tags_count()
        {
            var group1 = new MetricGroup<SimpleCounter>(tags => new SimpleCounter(tags), "key1");
            var group2 = new MetricGroup<SimpleCounter>(tags => new SimpleCounter(tags), "key1", "key2");

            group1.For("a").Value = 1;
            group2.For("a", "a").Value = 2;

            group1.For("a").Value.Should().Be(1);
            group2.For("a", "a").Value.Should().Be(2);

            group1.For("a")
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("key1", "a"));
            group2.For("a", "a")
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("key1", "a").Append("key2", "a"));
        }

        private class SimpleCounter
        {
            public readonly MetricTags Tags;
            public int Value;

            public SimpleCounter(MetricTags tags)
            {
                Tags = tags;
            }
        }
    }
}