using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;
using TPREmployeePayLibrary.Services;
using Xunit;

namespace TPREmployeePayLibrary.Tests
{
    public class PermanentEmployeeServicesTests
    {
        [Theory]
        [InlineData(15000, 0, 15000)]
        [InlineData(33333, 707, 34040)]
        public void CalculateAnnualDay_Returns_Expected_Result(decimal annualSalary, decimal annualBonus, decimal expectedResult)
        {
            var employee = new PermanentEmployee("Ben", annualSalary, annualBonus, DateTimeOffset.UtcNow);

            var actualResult = employee.CalculateAnnualPay();

            Assert.Equal(expectedResult, actualResult, 2);
        }

        [Theory]
        [InlineData(67830, 37.27)]
        [InlineData(22000, 12.09)]
        [InlineData(65432109.99, 35951.71)]
        public void CalculateHourlyPay_Returns_Expected_Result(decimal annualSalary, decimal expectedResult) 
        {
            var employee = new PermanentEmployee("Richard", annualSalary, 44, DateTimeOffset.UtcNow);

            var actualResult = employee.CalculateHourlyPay();

            Assert.Equal(expectedResult, actualResult,2);
        }
    }
}
