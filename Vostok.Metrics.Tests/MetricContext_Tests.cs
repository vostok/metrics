using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Vostok.Metrics.Tests
{
    [TestFixture]
    internal class MetricContext_Tests
    {
        private MetricContext context;

        [SetUp]
        public void TestSetup()
            => context = new MetricContext(new MetricContextConfig(Substitute.For<IMetricEventSender>()));

        [Test]
        public void Should_implement_annotation_context()
            => context.AsAnnotationContext().Should().NotBeNull();

        [Test]
        public void Should_implement_annotation_context_if_enriched_with_tags()
            => context.WithTag("key", "value").AsAnnotationContext().Should().NotBeNull();
    }
}
