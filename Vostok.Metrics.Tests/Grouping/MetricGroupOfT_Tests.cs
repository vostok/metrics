using System;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Models;

// ReSharper disable ObjectCreationAsStatement
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Vostok.Metrics.Tests.Grouping
{
    [TestFixture]
    internal class MetricGroupOfT_Tests
    {
        [Test]
        public void Should_use_fabric_for_create_new_or_return_existing()
        {
            var group = new MetricGroup<Model2, SimpleCounter>(tags => new SimpleCounter(tags));

            group.For(new Model2("a", "b1")).Value = 1;
            group.For(new Model2("a", "b2")).Value = 2;

            group.For(new Model2("a", "b1")).Value.Should().Be(1);
            group.For(new Model2("a", "b2")).Value.Should().Be(2);

            group.For(new Model2("a", "b1"))
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("Prop1", "a").Append("Prop2", "b1"));
            group.For(new Model2("a", "b2"))
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("Prop1", "a").Append("Prop2", "b2"));
        }

        [Test]
        public void Should_create_new_for_different_dynamic_tags_count()
        {
            var group1 = new MetricGroup<Model1, SimpleCounter>(tags => new SimpleCounter(tags));
            var group2 = new MetricGroup<Model2, SimpleCounter>(tags => new SimpleCounter(tags));

            group1.For(new Model1("a")).Value = 1;
            group2.For(new Model2("a", "a")).Value = 2;

            group1.For(new Model1("a")).Value.Should().Be(1);
            group2.For(new Model2("a", "a")).Value.Should().Be(2);

            group1.For(new Model1("a"))
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("Prop1", "a"));
            group2.For(new Model2("a", "a"))
                .Tags.Should()
                .BeEquivalentTo(
                    MetricTags.Empty.Append("Prop1", "a").Append("Prop2", "a"));
        }

        [Test]
        public void Ctor_should_throw_when_model_parameter_does_not_have_any_tag_properties()
        {
            Action action = () => new MetricGroup<string, string>(_ => string.Empty);

            action.Should().Throw<ArgumentException>();
        }

        private class Model1
        {
            public Model1(string prop1)
            {
                Prop1 = prop1;
            }

            [MetricTag(1)]
            public string Prop1 { get; }
        }

        private class Model2
        {
            public Model2(string prop1, string prop2)
            {
                Prop1 = prop1;
                Prop2 = prop2;
            }

            [MetricTag(1)]
            public string Prop1 { get; }

            [MetricTag(2)]
            public string Prop2 { get; }
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