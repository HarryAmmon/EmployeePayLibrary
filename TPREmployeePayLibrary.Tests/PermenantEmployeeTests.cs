using System;
using TPREmployeePayLibrary.Entities;
using Xunit;

namespace TPREmployeePayLibrary.Tests
{
    public class PermanentEmployeeTests
    {
        private readonly PermanentEmployee _employee;

        public PermanentEmployeeTests()
        {
            _employee = new PermanentEmployee("Harry", 1000.00m, 10.00m, DateTimeOffset.UtcNow);
        }

        [Fact]
        public void Can_Create_Permanent_Employee()
        {
            // Assert
            Assert.NotNull(_employee);
        }

        [Fact]
        public void Can_Not_Create_Permanent_Employee_With_Negative_Salary()
        {
            // Arrange
            var employeeName = "Harry";
            var annualSalary = -1000.22m;
            var annualBonus = 1000;
            var startDate = DateTimeOffset.UtcNow;
            
            Assert.Throws<Exception>(() => new PermanentEmployee(employeeName, annualSalary, annualBonus, startDate));
        }

        [Fact]
        public void Can_Not_Create_Permanent_Employee_With_Negative_AnnualBonus()
        {
            // Arrange
            var employeeName = "Harry";
            var annualSalary = 1000.21m;
            var annualBonus = -568534;
            var startDate = DateTimeOffset.UtcNow;

            // Assert
            Assert.Throws<Exception>(() => new PermanentEmployee(employeeName, annualSalary, annualBonus, startDate));
        }

        [Fact]
        public void Permanent_Employee_Will_Have_Todays_Date_If_StartDate_Not_Specified()
        {
            // Arrange
            var employee = new PermanentEmployee("Harry");
            var expectedDate = DateTimeOffset.UtcNow.Date;

            // Act
            var actualDate = employee.StartDate;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void Permanent_Employee_Will_Have_MinValue_End_Date_By_Default()
        {
            // Act
            var employee = new PermanentEmployee("Harry", 33000, 3000, new DateTimeOffset(2019, 10, 9, 0, 0, 0, TimeSpan.Zero));

            // Assert
            Assert.Equal(DateTimeOffset.MinValue, employee.EndDate);
        }

        [Fact]
        public void PermanentEmployee_Can_Have_StartDate_Specified()
        {
            // Arrange
            var employeeName = "Harry";
            var annualSalary = 20000;
            var annualBonus = 2000;
            var startDate = new DateTimeOffset(2019, 10, 8, 0, 0, 0, 0, TimeSpan.Zero);

            // Act
            var employee = new PermanentEmployee(employeeName, annualSalary, annualBonus, startDate);
            var actualStartDate = employee.StartDate;

            // Assert
            Assert.Equal(startDate, actualStartDate);
        }

        [Fact]
        public void Permanent_Employee_Can_Have_Name()
        {
            // Arrange
            var expectedName = "Harry";

            // Act
            var actualName = _employee.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Permanent_Employee_Can_Have_AnnualSalary()
        {
            // Arrange
            var expectedAnnualSalary = 1000.00m;

            // Act
            var actualAnnualSalary = _employee.AnnualSalary;

            // Assert
            Assert.Equal(expectedAnnualSalary, actualAnnualSalary);
        }

        [Fact]
        public void Permanent_Employee_Can_Have_AnnualBonus()
        {
            // Arrange
            var expectedBonusSalary = 10.00m;

            // Act
            var actualBonusSalary = _employee.AnnualBonus;

            // Assert
            Assert.Equal(expectedBonusSalary, actualBonusSalary);
        }

        [Fact]
        public void Employee_Is_Of_Type_Permanent()
        {
            // Arrange
            var expectedType = PermanentEmployee.EmployeeType.Permanent;

            // Act
            var actualType = _employee.Type;

            // Assert
            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void Employee_Has_An_ID_Of_Zero()
        {
            // Arrange

            // Act
            var actualId = _employee.EmployeeID;

            // Assert
            Assert.Equal(0, actualId);
        }

    }
}