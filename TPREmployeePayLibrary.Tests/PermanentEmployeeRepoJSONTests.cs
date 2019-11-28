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

namespace TPREmployeePaySolution.Tests
{
    public class PermanentEmployeeRepoJSONTests
    {
        private readonly PermanentEmployeeRepositoryJSON _repo;
        private readonly PermTestHelper _helper; 
        private readonly Mock<ILog> _log;
        private readonly string JSONPath = @"..\employeeData\permanentEmployee.json";

        public PermanentEmployeeRepoJSONTests()
        {
            _log = new Mock<ILog>();
            _repo = new PermanentEmployeeRepositoryJSON();
            _helper = new PermTestHelper(_repo);
        }

        [Fact]
        public void Can_Add_New_Permanent_Employee()
        {
            // Arrange
            var employee = new PermanentEmployee("Harry Smith", 20000, 111, new DateTimeOffset(2019, 6, 2, 0, 0, 0, TimeSpan.Zero));
            var expectedResult = employee.EmployeeID;

            // Act
            _repo.CreatePermanentEmployee(employee);
            var actualResult = _helper.SearchJSONForPermanentEmployee(employee.EmployeeID).EmployeeID;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Can_Delete_Permanent_Employee()
        {
            // Arrange
            var employeeToDelete = _helper.SearchJSONForPermanentEmployee("Nicole Perkins");


            // Act
            _repo.DeletePermanentEmployee(employeeToDelete.EmployeeID);
            var expectedResult = _helper.SearchJSONForPermanentEmployee(employeeToDelete.EmployeeID);
            // 
            Assert.Null(expectedResult);
        }

        [Fact]
        public void Can_Read_All_PermanentEmployees()
        {
            // Arrange

            // Act
            var list = _repo.ReadAllPermanentEmployees();

            // Assert
            Assert.NotNull(list);
        }

        [Fact]
        public void Can_Read_Permanent_Employee_Based_On_ID()
        {
            // Arrange
            var employee = _helper.SearchJSONForPermanentEmployee("Suzanne Hunnisett");

            // Act
            var actualResult = _repo.ReadPermanentEmployee(employee.EmployeeID);

            // Assert
            Assert.Equal(employee.EmployeeID, actualResult.EmployeeID);
        }

         
    }
}
