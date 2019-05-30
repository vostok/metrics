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
        public void SetQuantiles_GetQuantiles_should_work_with_not_empty_quantiles()
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
        public void Get_Set_AggregatePeriod_and_Get_Set_AggregateLag_should_works()
        {
            var dict = new Dictionary<string, string>()
                .SetAggregateLag(1.Seconds())
                .SetAggregatePeriod(2.Seconds());

            dict.GetAggregateLag().Should().Be(1.Seconds());
            dict.GetAggregatePeriod().Should().Be(2.Seconds());
        }

        [Test]
        public void Get_AggregatePeriod_AggregateLag_should_be_null_for_null_aggregation_parameters()
        {
            ((Dictionary<string, string>)null).GetAggregatePeriod().Should().BeNull();
            ((Dictionary<string, string>)null).GetAggregateLag().Should().BeNull();
        }

        [Test]
        public void Get_AggregatePeriod_AggregateLag_should_be_null_for_empty_aggregation_parameters()
        {
            new Dictionary<string, string>().GetAggregatePeriod().Should().BeNull();
            new Dictionary<string, string>().GetAggregateLag().Should().BeNull();
        }

        [Test]
        public void Get_AggregatePeriod_AggregateLag_should_be_null_for_null_quantiles()
        {
            new Dictionary<string, string> { { "_quantiles", null } }.GetAggregatePeriod().Should().BeNull();
            new Dictionary<string, string> { { "_quantiles", null } }.GetAggregateLag().Should().BeNull();
        }
    }
}