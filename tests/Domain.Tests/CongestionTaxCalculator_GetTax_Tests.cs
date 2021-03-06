using Domain.Models;
using Domain.Rules.Calculator;
using Domain.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Tests
{
    public class CongestionTaxCalculator_GetTax_Tests : IClassFixture<ServiceProviderFixture>
    {
        ServiceProviderFixture _fixture;

        public CongestionTaxCalculator_GetTax_Tests(ServiceProviderFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetTaxInputData))]
        public void WithVehicleAndDateTimes_ReturnTax(IVehicle vehicle, DateTime[] dates, int expectedTax)
        {
            // Arrange

            var calculator = _fixture.ServiceProvider.GetRequiredService<CalculatorRulesEngine>();
            var command = new CongestionCommand()
            {
                Dates = dates,
                Vehicle = vehicle,
            };

            // Act

            var result = calculator.Execute(command);

            // Assert

            Assert.Equal(expectedTax, result);
        }

        public static IEnumerable<object> GetTaxInputData =>
        new List<object>
        {
            new object[] {
                 new Car(),
                 new DateTime[0], // should work with empty array
                 0
             },
             new object[] { 
                 new Car(), 
                 new DateTime[] { new DateTime(2013, 1, 1) }, // is holiday 
                 0
             },
             new object[] {
                 null, // by default must be as car
                 new DateTime[] { new DateTime(2013, 1, 2, 6, 0, 0) },
                 8
             },
             new object[] {
                 new Motorbike(), // is free type
                 new DateTime[] { new DateTime(2013, 1, 2, 6, 0, 0) },
                 0
             },
             new object[] {
                 new Car(),
                 new DateTime[] { 
                     new DateTime(2013, 1, 2, 6, 0, 0) 
                 },
                 8
             },
             new object[] {
                 new Car(),
                 new DateTime[] { // difference longer than hour
                     new DateTime(2013, 1, 2, 6, 0, 0),
                     new DateTime(2013, 1, 2, 7, 20, 0)
                 },
                 18 + 8
             },
             new object[] {
                 new Car(),
                 new DateTime[] { // difference is hour
                     new DateTime(2013, 1, 2, 6, 0, 0),
                     new DateTime(2013, 1, 2, 7, 0, 0)
                 },
                 18
             },
             new object[] {
                 new Car(),
                 new DateTime[] { // difference is less than hour
                     new DateTime(2013, 1, 2, 6, 1, 0),
                     new DateTime(2013, 1, 2, 6, 59, 0)
                 },
                 Math.Max(8, 13)
             },
             new object[] {
                 new Car(),
                 new DateTime[] { // difference is less than hour and more than 2 times
                     new DateTime(2013, 1, 2, 6, 1, 0),
                     new DateTime(2013, 1, 2, 6, 2, 0),
                     new DateTime(2013, 1, 2, 6, 3, 0),
                     new DateTime(2013, 1, 2, 6, 59, 0)
                 },
                 13
             },
             new object[] {
                 new Car(),
                 new DateTime[] {
                     new DateTime(2013, 1, 2, 6, 30, 0), // cost is 13
                     new DateTime(2013, 1, 2, 7, 35, 0), // cost is 18
                     new DateTime(2013, 1, 2, 9, 0, 0), // cost is 8
                     new DateTime(2013, 1, 2, 15, 0, 0), // cost is 13
                     new DateTime(2013, 1, 2, 16, 1, 0), // cost is 18
                 },
                 60 // max is 60
             },
        };
    }
}
