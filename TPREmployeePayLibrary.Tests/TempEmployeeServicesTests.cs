using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;
using TPREmployeePayLibrary.Services;
using Xunit;
namespace TPREmployeePayLibrary.Tests
{
    public class TempEmployeeServicesTests
    {
        [Theory]
        [InlineData(-14,7,150)]
        [InlineData(-22,-1,150)]
        public void CalculateAnnualPay_Returns_Expected_Result(int startDateOffset, int endDateOffset, decimal expectedResult)
        {
            // Arrange
            var employee = new TempEmployee("Jeff",10,DateTimeOffset.UtcNow.AddDays(startDateOffset));
            employee.EndDate = DateTimeOffset.UtcNow.AddDays(endDateOffset);
            // Act
            var actualResult = employee.CalculateAnnualPay();

            // Assert
            Assert.Equal(expectedResult, actualResult, 2);

        }

        [Theory]
        [InlineData(28,4)]
        [InlineData(42,6)]
        public void CalculateHourlyPay_Returns_Expected_Result(decimal dailyRate, decimal expectedResult)
        {
            var employee = new TempEmployee("Peter", dailyRate, DateTimeOffset.UtcNow);

            var actualResult = employee.CalculateHourlyPay();

            Assert.Equal(expectedResult, actualResult, 2);
        }
    }
}
