using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Vostok.Metrics.Primitives.Timer;

namespace Vostok.Metrics.Tests.Primitives.Timer
{
    [TestFixture]
    internal class AggregationParametersExtensions_Tests
    {
        [Test]
        public void Set_Set_Quantiles_should_work_with_not_empty_quantiles()
        {
            var quantiles = new[] {0, 0.33, 0.44, 1};
            var parameters = new Dictionary<string, string>()
                .SetQuantiles(quantiles);
            var result = parameters.GetQuantiles();
            result.Should().BeEquivalentTo(quantiles);
        }

        [Test]
        public void SetQuantiles_GetQuantiles_should_work_with_empty_quantiles()
        {
            var quantiles = new double[0];
            var parameters = new Dictionary<string, string>()
                .SetQuantiles(quantiles);
            var result = parameters.GetQuantiles();
            result.Should().BeEquivalentTo(quantiles);
        }

        [Test]
        public void GetQuantiles_should_be_null_for_null_aggregation_parameters()
        {
            ((Dictionary<string, string>)null).GetQuantiles().Should().BeNull();
        }

        [Test]
        public void GetQuantiles_should_be_null_for_empty_aggregation_parameters()
        {
            new Dictionary<string, string>().GetQuantiles().Should().BeNull();
        }

        [Test]
        public void GetQuantiles_should_be_null_for_null_quantiles()
        {
            new Dictionary<string, string> {{"_quantiles", null}}.GetQuantiles().Should().BeNull();
        }

        [Test]
        public void GetQuantiles_should_be_empty_for_empty_quantiles()
        {
            new Dictionary<string, string> { { "_quantiles", "" } }.GetQuantiles().Should().BeEmpty();
        }

        [Test]
        public void Get_Set_AggregationPeriod_and_Get_Set_AggregationLag_should_works()
        {
            var dict = new Dictionary<string, string>()
                .SetAggregationLag(1.Seconds())
                .SetAggregationPeriod(2.Seconds());

            dict.GetAggregationLag().Should().Be(1.Seconds());
            dict.GetAggregationPeriod().Should().Be(2.Seconds());
        }

        [Test]
        public void Get_AggregationPeriod_AggregationLag_should_be_null_for_null_aggregation_parameters()
        {
            ((Dictionary<string, string>)null).GetAggregationPeriod().Should().BeNull();
            ((Dictionary<string, string>)null).GetAggregationLag().Should().BeNull();
        }

        [Test]
        public void Get_AggregationPeriod_AggregationLag_should_be_null_for_empty_aggregation_parameters()
        {
            new Dictionary<string, string>().GetAggregationPeriod().Should().BeNull();
            new Dictionary<string, string>().GetAggregationLag().Should().BeNull();
        }

        [Test]
        public void Get_AggregationPeriod_AggregationLag_should_be_null_for_null_quantiles()
        {
            new Dictionary<string, string> { { "_quantiles", null } }.GetAggregationPeriod().Should().BeNull();
            new Dictionary<string, string> { { "_quantiles", null } }.GetAggregationLag().Should().BeNull();
        }

        [Test]
        public void Get_Set_HistogramBucket_should_works()
        {
            var bucket = new HistogramBucket(123.4, 567.8);
            var dict = new Dictionary<string, string>()
                .SetHistogramBucket(bucket);

            dict.GetHistogramBucket().Should().BeEquivalentTo(bucket);
        }

        [Test]
        public void Get_HistogramBucket_should_be_null_for_null_aggregation_parameters()
        {
            ((Dictionary<string, string>)null).GetHistogramBucket().Should().BeNull();
        }

        [Test]
        public void Get_HistogramBucket_should_be_null_for_empty_aggregation_parameters()
        {
            new Dictionary<string, string>().GetHistogramBucket().Should().BeNull();
        }

        [Test]
        public void Get_HistogramBucket_should_be_null_for_null_bounds()
        {
            new Dictionary<string, string> { { "_lowerBound", null }, { "_upperBound", null } }.GetHistogramBucket().Should().BeNull();
            new Dictionary<string, string> { { "_lowerBound", null }, { "_upperBound", "42" } }.GetHistogramBucket().Should().BeNull();
            new Dictionary<string, string> { { "_lowerBound", "42" }, { "_upperBound", null } }.GetHistogramBucket().Should().BeNull();
        }
    }
}