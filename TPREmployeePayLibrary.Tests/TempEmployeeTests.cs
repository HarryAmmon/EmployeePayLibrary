using System;
using TPREmployeePayLibrary.Entities;
using Xunit;

namespace TPREmployeePayLibrary.Tests
{
    public class TempEmployeeTests
    {
        private readonly TempEmployee _employee;
        public TempEmployeeTests()
        {
            _employee = new TempEmployee("Harry", 8.22m, DateTimeOffset.UtcNow);
        }

        [Fact]
        public void Can_Create_Temp_Employee()
        {
            // Assert
            Assert.NotNull(_employee);
        }

        [Fact]
        public void Cant_Create_Temp_Employee_With_Negative_Daily_Rate()
        {
            // Arrange
            var employeeName = "Harry";
            var dailyRate = -22.22m;
            var startDate = DateTimeOffset.UtcNow;

            // Act

            // Assert
            Assert.Throws<Exception>(() => new TempEmployee(employeeName, dailyRate, startDate));
        }

        [Fact]
        public void TempEmployee_Will_Have_Todays_Date_If_StartDate_Not_Specified()
        {
            // Arrange
            var employee = new TempEmployee("Harry");
            var expectedDate = DateTimeOffset.UtcNow.Date;

            // Act
            var actualDate = employee.StartDate;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void TempEmployee_Can_Have_StartDate_Specified()
        {
            // Arrange
            var employeeName = "Harry";
            var dailyRate = 22.22m;
            var expectedDate = new DateTimeOffset(2019, 10, 7, 0, 0, 0, TimeSpan.Zero);

            // Act
            var employee = new TempEmployee(employeeName, dailyRate, expectedDate);
            var actualDate = employee.StartDate;

            //
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void TempEmployee_Will_Have_MinValue_EndDate_By_Default()
        {
            // Act
            var employee = new TempEmployee("Harry", 5000.2m, new DateTimeOffset(2019, 10, 9, 0, 0, 0, TimeSpan.Zero));

            // Assert
            Assert.Equal(DateTimeOffset.MinValue, employee.EndDate);
        }

        [Fact]
        public void Temp_Employee_Can_Have_Name()
        {
            // Act
            var actualName = _employee.Name;

            // Assert
            Assert.NotNull(actualName);
        }

        [Fact]
        public void Temp_Employee_Can_Have_DailyRate()
        {
            var expectedDailyRate = 8.22m;

            // Act
            var actualDailyRate = _employee.DailyRate;

            // Assert
            Assert.Equal(expectedDailyRate, actualDailyRate);
        }

        [Fact]
        public void Temp_Employee_Is_Of_Type_Temp()
        {
            // Arrange
            var expectedType = TempEmployee.EmployeeType.TempContractor;

            // Act
            var actualType = _employee.Type;

            // Assert
            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void Temp_Employee_Has_Id_Of_Zero()
        {
            // Arrange
            var actualId = _employee.EmployeeID;

            // Assert
            Assert.Equal(0, actualId);
        }
    }
}
