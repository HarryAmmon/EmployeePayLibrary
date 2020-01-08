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
                "INSERT INTO Employees (Name, StartDate, EndDate, AnnualSalary, AnnualBonus) VALUES(@Name, @StartDate, @EndDate, @AnnualSalary, @AnnualBonus)" +
                "SELECT CAST(SCOPE_IDENTITY() as INT)";

            var result =  await _db.QueryAsync<int>(sql, employee);
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

        public async Task<bool> DeletePermanentEmployeeAsync(int id)
        {
            var sql =
                "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
            var result = await _db.ExecuteAsync(sql, new { EmployeeID = id });

            if(result == 0)
            {
                return false;
            }
            return true;
        }

        public List<PermanentEmployee> ReadAllPermanentEmployees()
         {
            var sql = "SELECT * FROM Employees";
            var result = _db.Query<PermanentEmployee>(sql);

            return result.ToList();
        }

        public async Task<List<PermanentEmployee>> ReadAllPermanentEmployeesAsync()
        {
            var sql = "SELECT * FROM Employees";
            var result = await _db.QueryAsync<PermanentEmployee>(sql);

            return result.ToList();
        }

        public PermanentEmployee ReadPermanentEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PermanentEmployee> ReadPermanentEmployeeAsync(int id)
        {
            var sql = "SELECT * FROM Employees " +
                "WHERE EmployeeId = @EmployeeId";

            var result = await _db.QueryAsync<PermanentEmployee>(sql, new { EmployeeId = id });

            return result.FirstOrDefault();
        }

        public bool UpdatePermanentEmployee(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<PermanentEmployee> UpdatePermanentEmployeeAsync(PermanentEmployee employee)
        {
            var sql = "UPDATE Employees " +
                        "SET Name = @Name, " +
                        "    EndDate = @EndDate, " +
                        "   AnnualSalary = @AnnualSalary, " +
                        "   AnnualBonus = @AnnualBonus ";// +
                        //"   TeamId = @TeamId, " +
                        //"   ManagerId = @ManagerId";

            var result = await _db.ExecuteAsync(sql, employee);

            if (result == 0) { return null; }
            
            return employee;
        }
    }
}