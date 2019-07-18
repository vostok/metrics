using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Models;

// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Vostok.Metrics.Tests.Grouping
{
    [TestFixture]
    internal class MetricValueExtractor_Tests
    {
        [Test]
        public void ExtractValue_should_extract_double_value()
        {
            MetricValueExtractor.ExtractValue(new DoubleValueModel {Prop1 = 1, Prop2 = 2, Prop3 = 3.3})
                .Should().Be(3.3);
        }

        [Test]
        public void ExtractValue_should_extract_nullable_double_value_if_filled()
        {
            MetricValueExtractor.ExtractValue(new NullableDoubleValueModel { Value = 42 })
                .Should().Be(42);

            MetricValueExtractor.ExtractValue(new NullableDoubleValueModel { Value = null })
                .Should().Be(null);
        }

        [Test]
        public void ExtractValue_should_extract_int_value()
        {
            MetricValueExtractor.ExtractValue(new IntValueModel { Value = 42 })
                .Should().Be(42);
        }

        [Test]
        public void ExtractValue_should_be_null_for_non_value_model()
        {
            MetricValueExtractor.ExtractValue(new NonValueModel { Value = 42 })
                .Should().BeNull();
        }

        [Test]
        public void ExtractValue_should_be_null_for_multi_value_model()
        {
            MetricValueExtractor.ExtractValue(new MultiValueModel { Value1 = 1, Value2 = 2})
                .Should().BeNull();
        }

        private class DoubleValueModel
        {
            [MetricTag(1)]
            public double Prop1 { get; set; }

            public double Prop2 { get; set; }

            [MetricValue]
            public double Prop3 { get; set; }
        }

        private class NullableDoubleValueModel
        {
            [MetricValue]
            public double? Value { get; set; }
        }

        private class IntValueModel
        {
            [MetricValue]
            public int Value { get; set; }
        }

        private class NonValueModel
        {
            public double Value { get; set; }
        }

        private class MultiValueModel
        {
            [MetricValue]
            public double Value1 { get; set; }

            [MetricValue]
            public double Value2 { get; set; }
        }
    }
}