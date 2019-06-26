using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Models;

namespace Vostok.Metrics.Tests.Models
{
    [TestFixture]
    internal class MetricTags_Tests
    {
        private MetricTag tag1;
        private MetricTag tag2;
        private MetricTag tag3;
        private MetricTag tag4;
        private MetricTag tag5;
        private MetricTags tags;

        [SetUp]
        public void TestSetup()
        {
            tag1 = new MetricTag("k1", "v1");
            tag2 = new MetricTag("k2", "v2");
            tag3 = new MetricTag("k3", "v3");
            tag4 = new MetricTag("k4", "v4");
            tag5 = new MetricTag("k5", "v5");
            tags = MetricTags.Empty;
        }

        [Test]
        public void Empty_tags_should_have_zero_count()
        {
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Empty_tags_should_have_zero_hashcode()
        {
            tags.GetHashCode().Should().Be(0);
        }

        [Test]
        public void Empty_tags_should_behave_as_empty_enumerable()
        {
            tags.Should().BeEmpty();
        }

        [Test]
        public void Tags_initialized_with_capacity_should_be_empty()
        {
            tags = new MetricTags(10);

            tags.Should().BeEmpty();
            tags.Count.Should().Be(0);
        }

        [Test]
        public void Tags_initialized_with_capacity_should_have_zero_hashcode()
        {
            tags = new MetricTags(10);

            tags.GetHashCode().Should().Be(0);
        }

        [Test]
        public void Tags_initialized_with_an_array_of_tags_should_contain_all_of_its_elements()
        {
            tags = new MetricTags(tag1, tag2, tag3);

            tags.Count.Should().Be(3);
            tags.Should().Equal(tag1, tag2, tag3);
        }

        [Test]
        public void Indexer_should_return_correct_values_for_valid_indices()
        {
            tags = new MetricTags(tag1, tag2, tag3);

            tags[0].Should().BeSameAs(tag1);
            tags[1].Should().BeSameAs(tag2);
            tags[2].Should().BeSameAs(tag3);
        }

        [Test]
        public void Indexer_should_throw_exception_for_invalid_indexes_when_filled()
        {
            tags = new MetricTags(tag1, tag2, tag3);

            Action action = () => Console.Out.WriteLine(tags[tags.Count]);

            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Indexer_should_throw_exception_for_invalid_indexes_when_empty()
        {
            Action action = () => Console.Out.WriteLine(tags[0]);

            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Append_should_keep_immutability()
        {
            var tags2 = tags.Append(tag1);
            var tags3 = tags2.Append(tag2);
            var tags4 = tags3.Append(tag3);

            tags.Should().BeEmpty();
            tags2.Should().Equal(tag1);
            tags3.Should().Equal(tag1, tag2);
            tags4.Should().Equal(tag1, tag2, tag3);
        }

        [Test]
        public void Append_should_keep_immutability_when_called_multiple_times()
        {
            tags = tags.Append(tag1);

            var tags1 = tags.Append(tag2);
            var tags2 = tags.Append(tag3);
            var tags3 = tags.Append(tag4);

            tags.Should().Equal(tag1);
            tags1.Should().Equal(tag1, tag2);
            tags2.Should().Equal(tag1, tag3);
            tags3.Should().Equal(tag1, tag4);
        }

        [Test]
        public void Append_should_keep_immutability_when_called_multiple_times_on_children()
        {
            tags = tags.Append(tag1);

            var tags1 = tags.Append(tag2);
            var tags2 = tags.Append(tag3);
            var tags3 = tags1.Append(tag4);
            var tags4 = tags1.Append(tag5);

            tags.Should().Equal(tag1);
            tags1.Should().Equal(tag1, tag2);
            tags2.Should().Equal(tag1, tag3);
            tags3.Should().Equal(tag1, tag2, tag4);
            tags4.Should().Equal(tag1, tag2, tag5);
        }

        [Test]
        public void Append_should_return_same_tags_when_appending_an_empty_list()
        {
            var newTags = tags.Append(new MetricTag[] {});

            newTags.Should().BeSameAs(tags);
        }

        [Test]
        public void Append_should_handle_lists()
        {
            tags = tags.Append(tag1);

            var tags2 = tags.Append(new[] {tag2, tag3});
            var tags3 = tags.Append(new[] {tag4, tag5});
            var tags4 = tags2.Append(new[] {tag4, tag5});

            tags.Should().Equal(tag1);
            tags2.Should().Equal(tag1, tag2, tag3);
            tags3.Should().Equal(tag1, tag4, tag5);
            tags4.Should().Equal(tag1, tag2, tag3, tag4, tag5);
        }

        [Test]
        public void Equals_should_return_true_for_equal_tags()
        {
            MetricTags.Empty.Equals(new MetricTags(10)).Should().BeTrue();
            MetricTags.Empty.Equals(new MetricTags()).Should().BeTrue();

            var tags1 = new MetricTags(tag1, tag3, tag5);
            var tags2 = new MetricTags(tag1, tag3, tag5);
            var tags3 = MetricTags.Empty.Append(tags2);
            var tags4 = MetricTags.Empty.Append(tag1).Append(tag3).Append(tag5);

            tags1.Equals(tags2).Should().BeTrue();
            tags1.Equals(tags3).Should().BeTrue();
            tags1.Equals(tags4).Should().BeTrue();
        }

        [Test]
        public void Equals_should_return_false_for_different_tags()
        {
            var tags1 = MetricTags.Empty;
            var tags2 = new MetricTags(tag1);
            var tags3 = new MetricTags(tag2);
            var tags4 = new MetricTags(tag1, tag2);
            var tags5 = new MetricTags(tag2, tag1);
            var tags6 = new MetricTags(tag2, tag1, tag3);

            new[] {tags1, tags2, tags3, tags4, tags5, tags6}.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void GetHashCode_should_return_same_values_for_equal_tags()
        {
            MetricTags.Empty.Equals(new MetricTags(10)).Should().BeTrue();
            MetricTags.Empty.Equals(new MetricTags()).Should().BeTrue();

            var tags1 = new MetricTags(tag1, tag3, tag5);
            var tags2 = new MetricTags(tag1, tag3, tag5);
            var tags3 = MetricTags.Empty.Append(tags2);
            var tags4 = MetricTags.Empty.Append(tag1).Append(tag3).Append(tag5);

            tags1.GetHashCode().Equals(tags2.GetHashCode()).Should().BeTrue();
            tags1.GetHashCode().Equals(tags3.GetHashCode()).Should().BeTrue();
            tags1.GetHashCode().Equals(tags4.GetHashCode()).Should().BeTrue();
        }

        [Test]
        public void GetHashCode_should_return_different_values_for_different_tags()
        {
            var tags1 = MetricTags.Empty;
            var tags2 = new MetricTags(tag1);
            var tags3 = new MetricTags(tag2);
            var tags4 = new MetricTags(tag1, tag2);
            var tags5 = new MetricTags(tag2, tag1);
            var tags6 = new MetricTags(tag2, tag1, tag3);

            new[] {tags1, tags2, tags3, tags4, tags5, tags6}.Select(t => t.GetHashCode()).Should().OnlyHaveUniqueItems();
        }
    }
}