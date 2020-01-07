using log4net;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TPREmployeePayLibrary.Entities;
using TPREmployeePayLibrary.Repository;
using TPREmployeePayLibrary.Tests;
using Xunit;

namespace TPREmployeePayLibrary.Tests
{
    public class PermanentEmployeeRepoJSONTests
    {
        private readonly PermanentEmployeeRepositoryJSON _repo;
        private readonly PermTestHelper _helper;
        private readonly Mock<ILog> _log;
        private readonly Mock<List<PermanentEmployee>> _list;
        private readonly string JSONPath = @"..\employeeData\permanentEmployee.json";

        public PermanentEmployeeRepoJSONTests()
        {
            _log = new Mock<ILog>();
            _repo = new PermanentEmployeeRepositoryJSON();
            _helper = new PermTestHelper(_repo);
            _list = new Mock<List<PermanentEmployee>>();
        }

        [Fact]
        public void Can_Add_New_Permanent_Employee()
        {
            // Arrange
            var employee = new PermanentEmployee("Harry Smith", 20000, 111, new DateTimeOffset(2019, 6, 2, 0, 0, 0, TimeSpan.Zero));
            var expectedResult = employee.EmployeeID;

            // Act
            var actualResult = _repo.CreatePermanentEmployee(employee).EmployeeID;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void An_Employee_With_No_Id_Will_Be_Assigned_Id_When_Added_To_Repo()
        {
            // Arrange
            var employee = new PermanentEmployee("Harry");
            employee.EmployeeID = 0;

            // Act
            var actualResult = _repo.CreatePermanentEmployee(employee).EmployeeID;

            // Assert
            Assert.NotEqual(0, actualResult);
        }

        [Fact]
        public void Can_Delete_Permanent_Employee()
        {
            // Arrange
            var employeeToDelete = _helper.SearchJSONForPermanentEmployee("Nicole Perkins");

            // Act
            var actualResult = _repo.DeletePermanentEmployee(employeeToDelete.EmployeeID);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Deleting_Employee_That_Does_Not_Exist_Returns_False()
        {
            // Arrange
            var employeeId = 1986;

            // Act
            var actualResult = _repo.DeletePermanentEmployee(employeeId);

            // Assert
            Assert.False(actualResult);
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
            var employeeID = _helper.SearchJSONForPermanentEmployee("Suzanne Hunnisett").EmployeeID;

            // Act
            var actualResult = _repo.ReadPermanentEmployee(employeeID).EmployeeID;

            // Assert
            Assert.Equal(employeeID, actualResult);
        }

        [Fact]
        public void Reading_Employee_That_Does_Not_Exist_Returns_Null()
        {
            // Arrange
            var employeeId = 23810;

            // Act
            var actualResult = _repo.ReadPermanentEmployee(employeeId);

            // Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public void Can_Update_Employee_Name()
        {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForPermanentEmployee("Devin Disney");

            // Act
            employeeToUpdate.Name = "Gary L";
            var actualResult = _repo.UpdatePermanentEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Can_Update_Employee_AnnualSalary()
        {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForPermanentEmployee("Margo Bailey");

            // Act
            employeeToUpdate.AnnualSalary = 8542.00m;
            var actualResult = _repo.UpdatePermanentEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void Can_Update_Employee_AnnualBonus()
        {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForPermanentEmployee("Ashton Botwright");

            // Act
            employeeToUpdate.AnnualBonus = 101.01m;
            var actualResult = _repo.UpdatePermanentEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
            
        }

        [Fact]
        public void Can_Update_Employee_EndDate() {
            // Arrange
            var employeeToUpdate = _helper.SearchJSONForPermanentEmployee("Devin Disney");

            // Act
            employeeToUpdate.EndDate = DateTimeOffset.UtcNow;
            var actualResult = _repo.UpdatePermanentEmployee(employeeToUpdate);

            // Assert
            Assert.True(actualResult);
        }        
    }
}
