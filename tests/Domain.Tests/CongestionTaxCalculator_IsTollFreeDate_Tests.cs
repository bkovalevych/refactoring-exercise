using Domain.Rules.DateRules;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Tests
{
    public class CongestionTaxCalculator_IsTollFreeDate_Tests
    {
        [Theory]
        [MemberData(nameof(FreeDates))]
        public void WithFreeDate_ReturnTrue(DateTime date)
        {
            // Arrange

            var dateRules = new DateRulesEngine();

            // Act

            var result = dateRules.Execute(date);

            // Assert

            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(NotFreeDates))]
        public void WithNotFreeDate_ReturnFalse(DateTime date)
        {
            // Arrange

            var dateRules = new DateRulesEngine();

            // Act

            var result = dateRules.Execute(date);

            // Assert

            Assert.True(result);
        }

        public static IEnumerable<object[]> FreeDates =>
        new List<object[]>
        {
             new object [] { new DateTime(2013, 1, 1) },
             new object[] { new DateTime(2013, 3, 28) },
             new object[] { new DateTime(2013, 3, 29) },
             new object[] { new DateTime(2013, 4, 1) },
             new object[] { new DateTime(2013, 4, 30) },
             new object[] { new DateTime(2013, 5, 1) },
             new object[] { new DateTime(2013, 5, 8) },
             new object[] { new DateTime(2013, 5, 9) },
             new object[] { new DateTime(2013, 6, 5) },
             new object[] { new DateTime(2013, 6, 6) },
             new object[] { new DateTime(2013, 6, 21) },
             new object[] { new DateTime(2013, 11, 1) },
             new object[] { new DateTime(2013, 12, 24) },
             new object[] { new DateTime(2013, 12, 25) },
             new object[] { new DateTime(2013, 12, 26) },
             new object[] { new DateTime(2013, 12, 31) }
        };

        public static IEnumerable<object> NotFreeDates =>
        new List<object>
        {
             new object[] { new DateTime(2013, 1, 3) },
             new object[] { new DateTime(2013, 3, 12) },
             new object[] { new DateTime(2013, 3, 11) }
        };
    }
}