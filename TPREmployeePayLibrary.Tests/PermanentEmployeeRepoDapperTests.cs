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
        public PermanentEmployeeRepoDapperTests()
        {
            _dbConnection = new Mock<IDbConnection>();
            _repo = new PermanentEmployeeRepositoryDapper(_dbConnection.Object);
        }

        [Fact]
        public void Can_Add_Permanent_Employee()
        {
            var employeeToAdd = new PermanentEmployee("Harry");

            var result = _repo.CreatePermanentEmployeeAsync(employeeToAdd).Result;

            Assert.Equal(employeeToAdd,result);
        }

    }
}