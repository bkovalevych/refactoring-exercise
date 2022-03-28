using Domain.Rules.FeeRules;
using Domain.Vehicles;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Tests
{
    public class CongestionTaxCalculator_GetTollFee_Tests
    {
        [Theory]
        [MemberData(nameof(DatesAndExpectedCosts))]
        public void WithDateAndCar_ReturnsCost(DateTime date, int expectedCost)
        {
            // Arrange

            var feeRules = new FeeRulesEngine();

            // Act

            var result = feeRules.Execute(date);

            // Assert

            Assert.Equal(expectedCost, result);
        }
        public static IEnumerable<object> DatesAndExpectedCosts =>
        new List<object>
        {
             new object[] { new DateTime(1, 1, 1, 6, 29, 30), 8 },
             new object[] { new DateTime(1, 1, 1, 7, 0, 0), 18 },
             new object[] { new DateTime(1, 1, 1, 5, 0, 0), 0 }
        };

    }
}
