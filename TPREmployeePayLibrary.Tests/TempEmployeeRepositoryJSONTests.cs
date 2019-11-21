using log4net;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TPREmployeePayLibrary.Entites;
using TPREmployeePayLibrary.Repository;
using TPREmployeePayLibrary.Tests;
using Xunit;

namespace TPREmployeePaySolution.Tests
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
        public void Can_Delete_Temp_Employee()
        {
            // Arrange
            var employeeToDelete = _helper.SearchJSONForTempEmployee("Seth Cox");

            // Act
            _repo.DeleteTempEmployee(employeeToDelete.EmployeeID);
            var expectedResult = _helper.SearchJSONForTempEmployee(employeeToDelete.EmployeeID);

            // Assert
            Assert.Null(expectedResult);
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
                
    }
}
