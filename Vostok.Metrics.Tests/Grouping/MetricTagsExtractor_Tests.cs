using FluentAssertions;
using NUnit.Framework;
using Vostok.Metrics.Grouping;
using Vostok.Metrics.Models;

// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Vostok.Metrics.Tests.Grouping
{
    [TestFixture]
    internal class MetricTagsExtractor_Tests
    {
        [Test]
        public void ExtractTags_should_extract_tags_from_model_properties_in_correct_order()
        {
            MetricTagsExtractor.ExtractTags(new Model(123, "foo", "bar"))
                .Should()
                .Equal(
                    new MetricTag("Prop2", "foo"),
                    new MetricTag("Prop3", "bar"),
                    new MetricTag("Prop1", "123"));
        }

        [Test]
        public void ExtractTags_should_tolerate_null_property_values()
        {
            MetricTagsExtractor.ExtractTags(new Model(123, null, null))
                .Should()
                .Equal(
                    new MetricTag("Prop2", "none"),
                    new MetricTag("Prop3", "none"),
                    new MetricTag("Prop1", "123"));
        }

        [Test]
        public void HasTags_should_return_true_for_types_with_metric_tag_attributes()
        {
            MetricTagsExtractor.HasTags(typeof(Model)).Should().BeTrue();
        }

        [Test]
        public void HasTags_should_return_false_for_types_with_metric_tag_attributes()
        {
            MetricTagsExtractor.HasTags(typeof(NonModel)).Should().BeFalse();
        }

        private class Model
        {
            public Model(int prop1, string prop2, string prop3)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Prop3 = prop3;
            }

            [MetricTag(3)]
            public int Prop1 { get; }

            [MetricTag(1)]
            public string Prop2 { get; }

            [MetricTag(2)]
            public string Prop3 { get; }
        }

        private class NonModel
        {
            public NonModel(int prop1, string prop2)
            {
                Prop1 = prop1;
                Prop2 = prop2;
            }

            public int Prop1 { get; }

            public string Prop2 { get; }
        }
    }
}