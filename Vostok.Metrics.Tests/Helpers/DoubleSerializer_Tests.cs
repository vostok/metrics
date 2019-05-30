using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Tests.Helpers
{
    [TestFixture]
    internal class DoubleSerializer_Tests
    {
        [Test]
        public void Serialize_should_works()
        {
            DoubleSerializer.Serialize(0.33).Should().Be("0.33");
        }

        [Test]
        public void Deserialize_should_works()
        {
            DoubleSerializer.Deserialize("0.33").Should().Be(0.33);
        }
    }
}