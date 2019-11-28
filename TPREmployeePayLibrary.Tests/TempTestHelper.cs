using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;
using TPREmployeePayLibrary.Repository;

namespace TPREmployeePayLibrary.Tests
{
    public class TempTestHelper
    {
        private ITempEmployeeRepo _repo;
        public TempTestHelper(ITempEmployeeRepo repo)
        {
            _repo = repo;
        }

        public TempEmployee SearchJSONForTempEmployee(string Name)
        {
            var employees = _repo.ReadAllTempEmployees();


            if (!employees.Exists(x => x.Name.Equals(Name))) { return null; }

            // Search for employee based on name
            var employee = employees.Find(x => x.Name.Equals(Name));

            return employee;
        }

        public TempEmployee SearchJSONForTempEmployee(Guid id)
        {
            var employees = _repo.ReadAllTempEmployees();

            if (!employees.Exists(x => x.EmployeeID.Equals(id))) { return null; }

            // Search for employee based on id
            var employee = employees.Find(x => x.EmployeeID.Equals(id));

            return employee;
        }
    }
}
