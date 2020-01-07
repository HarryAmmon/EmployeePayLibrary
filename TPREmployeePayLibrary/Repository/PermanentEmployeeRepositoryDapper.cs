using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace TPREmployeePayLibrary.Repository
{
    public class PermanentEmployeeRepositoryDapper : IPermanentRepo
    {
        private IDbConnection _db;

        public PermanentEmployeeRepositoryDapper(IDbConnection dbConnection)
        {
            _db = dbConnection;
        }

        public async Task<PermanentEmployee> CreatePermanentEmployeeAsync(PermanentEmployee employee)
        {
            var sql =
                "INSERT INTO Employee (Name, StartDate, EndDate, ManagerId, TeamId, AnnualSalary, AnnualBonus) VALUES(@Name, @StartDate, @EndDate, @ManagerId, @TeamId, @AnnualSalary, @AnnualBonus)" +
                "SELECT CAST(SCOPE_IDENTITY() as INT)";

            var result =  _db.Query<int>(sql, employee);
            employee.EmployeeID = result.FirstOrDefault();

            return employee;
        }

        public PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }

        public bool DeletePermanentEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePermanentEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<PermanentEmployee> ReadAllPermanentEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<List<PermanentEmployee>> ReadAllPermanentEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public PermanentEmployee ReadPermanentEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PermanentEmployee> ReadPermanentEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePermanentEmployee(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }

        public Task<PermanentEmployee> UpdatePermanentEmployeeAsync(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }

        PermanentEmployee IPermanentWriteable.CreatePermanentEmployee(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }
    }
}
