using Domain.Rules.FeeRules;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Tests
{
    public class CongestionTaxCalculator_GetTollFee_Tests : IClassFixture<ServiceProviderFixture>
    {
        ServiceProviderFixture _fixture;

        public CongestionTaxCalculator_GetTollFee_Tests(ServiceProviderFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(DatesAndExpectedCosts))]
        public void WithDateAndCar_ReturnsCost(DateTime date, int expectedCost)
        {
            // Arrange

            var calculator = new FeeRulesEngine(_fixture.ServiceProvider);
            
            // Act

            var result = calculator.Execute(date);

            // Assert

            Assert.Equal(expectedCost, result);
        }
        public static IEnumerable<object> DatesAndExpectedCosts =>
        new List<object>
        {
             new object[] { new DateTime(1, 1, 1, 6, 29, 30), 8 },
             new object[] { new DateTime(1, 1, 1, 6, 29, 30), 8 },
             new object[] { new DateTime(1, 1, 1, 5, 0, 0), 0 }
        };

    }
}
