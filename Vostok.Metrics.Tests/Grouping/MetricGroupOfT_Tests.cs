using System;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Grouping;

// ReSharper disable ObjectCreationAsStatement

namespace Vostok.Metrics.Tests.Grouping
{
    [TestFixture]
    internal class MetricGroupOfT_Tests
    {
        [Test]
        public void Ctor_should_throw_when_model_parameter_does_not_have_any_tag_properties()
        {
            Action action = () => new MetricGroup<string, string>(_ => string.Empty);

            action.Should().Throw<ArgumentException>();
        }
    }
}