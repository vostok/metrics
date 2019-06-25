using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Helpers;

namespace Vostok.Metrics.Tests.Helpers
{
    [TestFixture]
    internal class TimeSpanSerializer_Tests
    {
        [Test]
        public void Serialize_should_works()
        {
            TimeSpanSerializer.Serialize(31.Seconds() + 123.Milliseconds()).Should().Be("0:00:00:31.1230000");
        }

        [Test]
        public void Deserialize_should_works()
        {
            TimeSpanSerializer.Deserialize("0:00:00:31.1230000").Should().Be(31.Seconds() + 123.Milliseconds());
        }
    }
}