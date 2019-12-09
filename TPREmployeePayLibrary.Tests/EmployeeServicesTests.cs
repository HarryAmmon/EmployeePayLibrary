using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;
using TPREmployeePayLibrary.Services;
using Xunit;

namespace TPREmployeePayLibrary.Tests
{
    public class EmployeeServicesTests
    {
        public readonly DateTimeOffset NextWeek = DateTimeOffset.UtcNow.AddDays(7).Date;
        //public readonly Mock<DateTimeOffset> _dateTimeOffset;
        public EmployeeServicesTests()
        {
            //_dateTimeOffset = new Mock<DateTimeOffset>();
        }

        [Fact]
        public void StartDate_After_Current_Date_WeeksWorked_Zero()
        {
            // Arrange
            var employee = new PermanentEmployee("Toby", 100.00m, 1m, DateTimeOffset.UtcNow.AddDays(7));
            var expectedResult = 0;

            // Act
            var actualResult = employee.CalcWeeksWorked();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CalcWeeksWorked_Returns_Expected_Result_When_EndDate_MinValue()
        {
            // Arrange
            var employee = new PermanentEmployee("Richard", 10.0m, 1.1m, DateTimeOffset.UtcNow.AddDays(-7));
            var expectedResult = (DateTimeOffset.Now - employee.StartDate).TotalDays / 7;
            // Act
            var actualResult = employee.CalcWeeksWorked();

            // Assert
            Assert.Equal(expectedResult, actualResult,2);
        }

        [Fact]
        public void CalcWeeksWorked_Returns_Expected_Result_When_EndDate_Is_After_StartDate()
        {
            // Arrange
            var employee = new PermanentEmployee("Richard", 10.0m, 1.1m, DateTimeOffset.UtcNow.AddDays(-7));
            employee.EndDate = DateTimeOffset.UtcNow.AddDays(-6);
            var expectedResult = (employee.EndDate - employee.StartDate).TotalDays / 7;

            // Act
            var actualResult = employee.CalcWeeksWorked();

            // Assert
            Assert.Equal(expectedResult, actualResult, 2);
        }
    }
}
