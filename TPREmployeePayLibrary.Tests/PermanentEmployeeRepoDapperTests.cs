using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;
using TPREmployeePayLibrary.Repository;
using TPREmployeePayLibrary.Entities;
using System.Data.SqlClient;
using Moq;

namespace TPREmployeePayLibrary.Tests
{
    public class PermanentEmployeeRepoDapperTests
    {
        private IPermanentRepo _repo;
        private Mock<IDbConnection> _dbConnection;
        private IDbConnection _HardCodedConnection;
        public PermanentEmployeeRepoDapperTests()
        {
            _HardCodedConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeePayData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _dbConnection = new Mock<IDbConnection>();
            //_repo = new PermanentEmployeeRepositoryDapper(_dbConnection.Object);
            _repo = new PermanentEmployeeRepositoryDapper(_HardCodedConnection);
        }

        [Fact]
        public void Can_Add_Permanent_Employee()
        {
            var employeeToAdd = new PermanentEmployee("Harry");

            var result = _repo.CreatePermanentEmployeeAsync(employeeToAdd).Result;

            Assert.Equal(employeeToAdd,result);
        }

        [Fact]
        public void Can_Delete_Permanent_Employee()
        {
            var employeeIdToDelete = 17;

            var result = _repo.DeletePermanentEmployeeAsync(employeeIdToDelete).Result;

            Assert.True(result);
        }

        [Fact]
        public void Can_Get_All_Permanent_Employees_As_List_()
        {
            var result = _repo.ReadAllPermanentEmployeesAsync().Result;

            Assert.IsType<List<PermanentEmployee>>(result);
        }

        [Fact]
        public void Can_Get_A_Permanent_Employee_By_Id()
        {
            var employeeToRead = 22;
            var expectedResult = "Harry";

            var result = _repo.ReadPermanentEmployeeAsync(employeeToRead).Result;

            var actualResult = result.Name;

            Assert.Equal(actualResult, expectedResult);

        }

        [Fact]
        public void Can_Update_A_Permanent_Employee_By_Id()
        {
            var employeeToUpdate = _repo.ReadPermanentEmployeeAsync(5).Result;
            var expectedAnnualSalary = 35000.00m;

            employeeToUpdate.AnnualSalary = expectedAnnualSalary;

            var result = _repo.UpdatePermanentEmployeeAsync(employeeToUpdate).Result;

            Assert.Equal(expectedAnnualSalary, result.AnnualSalary);
        }
    }
}