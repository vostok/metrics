using System;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Models;

// ReSharper disable ObjectCreationAsStatement
// ReSharper disable AssignNullToNotNullAttribute

namespace Vostok.Metrics.Tests.Models
{
    [TestFixture]
    internal class MetricTag_Tests
    {
        [Test]
        public void Should_not_allow_null_keys()
        {
            Action action = () => new MetricTag(null, "value");

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_not_allow_null_values()
        {
            Action action = () => new MetricTag("key", null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_have_case_sensitive_equality()
        {
            var tag1 = new MetricTag("key1", "value");
            var tag2 = new MetricTag("key1", "value");
            var tag3 = new MetricTag("Key1", "value");
            var tag4 = new MetricTag("key2", "value");

            tag1.Should().Be(tag2);
            tag1.Should().NotBe(tag3);
            tag1.Should().NotBe(tag4);
            tag3.Should().NotBe(tag4);
        }

        [Test]
        public void Should_have_case_sensitive_hash_code()
        {
            var tag1 = new MetricTag("key1", "value");
            var tag2 = new MetricTag("Key1", "value");
            var tag3 = new MetricTag("key1", "Value");

            tag1.GetHashCode().Should().NotBe(tag2.GetHashCode());
            tag1.GetHashCode().Should().NotBe(tag3.GetHashCode());
            tag2.GetHashCode().Should().NotBe(tag3.GetHashCode());
        }
    }
}