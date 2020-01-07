using log4net;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TPREmployeePayLibrary.Entities;
using TPREmployeePayLibrary.Repository;
using TPREmployeePayLibrary.Tests;
using Xunit;

namespace TPREmployeePayLibrary.Tests
{
    public class TempEmployeeRepoJSONTest
    {
        private readonly TempEmployeeRepositoryJSON _repo;
        private readonly Mock<ILog> _log;
        private readonly string JSONPath = @"..\employeeData\tempEmployee.JSON";
        private readonly TempTestHelper _helper;

        public TempEmployeeRepoJSONTest()
        {
            _log = new Mock<ILog>();
            _repo = new TempEmployeeRepositoryJSON();
            _helper = new TempTestHelper(_repo);
        }

        [Fact]
        public void Can_Add_New_Temp_Employee()
        {
            // Arrange
            var employee = new TempEmployee("Josh Smith", 19.2m, new DateTimeOffset(2012, 8, 1, 0, 0, 0, TimeSpan.Zero));
            var expectedResult = employee.EmployeeID;

            // Act
            _repo.CreateTempEmployee(employee);
            var actualResult = _helper.SearchJSONForTempEmployee(employee.EmployeeID).EmployeeID;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void An_Employee_With_No_Id_Will_Be_Assigned_If_When_Added_To_Repo()
        {
            // Arrange
            var employee = new TempEmployee("Harry");
            employee.EmployeeID = 0;

            // Act
            var actualResult = _repo.CreateTempEmployee(employee).EmployeeID;

            // Arrange
            Assert.NotEqual(0, actualResult);
        }

        [Fact]
        public void Can_Delete_Temp_Employee()
        {
            // Arrange
            var employeeToDelete = _helper.SearchJSONForTempEmployee("Seth Cox").EmployeeID;

            // Act
            var expectedResult = _repo.DeleteTempEmployee(employeeToDelete);

            // Assert
            Assert.True(expectedResult);
        }

        [Fact]
        public void Deleting_Employee_That_Does_Not_Exist_Returns_False()
        {
            // Arrage
            var employeeToDelete = 5816;

            // Act
            var expectedResult = _repo.DeleteTempEmployee(employeeToDelete);

            // Assert
            Assert.False(expectedResult);
        }

        [Fact]
        public void Can_Read_All_Temp_Employees()
        {
            // Arrange

            // Act
            var list = _repo.ReadAllTempEmployees();

            // Assert
            Assert.NotNull(list);
        }

        [Fact]
        public void Can_Read_Temp_Employee_Based_On_ID()
        {
            // Arrange
            var employee = _helper.SearchJSONForTempEmployee("Duncan Queen");

            // Act
            var actualResult = _repo.ReadTempEmployee(employee.EmployeeID);

            // Assert
            Assert.Equal(employee.EmployeeID, actualResult.EmployeeID);
        }

        [Fact]
        public void Reading_Employee_That_Does_Not_Exist_Returns_Null()
        {
            // Arrange
            var employeeId = 3523;

            // Act
            var actualResult = _repo.ReadTempEmployee(employeeId);

            // Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public void Can_Update_Employee_Name()
        {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForTempEmployee("Duncan Queen");

            // Act
            employeeToUpdate.Name = "Gary L";
            var actualResult = _repo.UpdateTempEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Can_Update_Employee_DailyRate()
        {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForTempEmployee("Tilly Duff");

            // Act
            employeeToUpdate.DailyRate = 8542.00m;
            var actualResult = _repo.UpdateTempEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
        }

        //[Fact]
        //public void Can_Update_Employee_AnnualBonus()
        //{
        //    // Arrange
        //    var employeeToUpdate = _helper.SearchJSONForTempEmployee("Ashton Botwright");

        //    // Act
        //    employeeToUpdate.AnnualBonus = 101.01m;
        //    var actualResult = _repo.UpdateTempEmployee(employeeToUpdate);

        //    // Assert
        //    Assert.True(actualResult);

        //}

        [Fact]
        public void Can_Update_Employee_EndDate()
        {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForTempEmployee("Patrick Maynard");

            // Act
            employeeToUpdate.EndDate = DateTimeOffset.UtcNow;
            var actualResult = _repo.UpdateTempEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
        }
    }
}

