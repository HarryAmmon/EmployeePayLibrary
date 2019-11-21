using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;
using TPREmployeePayLibrary.Repository;

namespace TPREmployeePayLibrary.Tests
{
    public class PermTestHelper
    {
        private IPermanentRepo _permRepo;

        public PermTestHelper(IPermanentRepo permRepo)
        {
            _permRepo = permRepo;
        }
        
        public PermanentEmployee SearchJSONForPermanentEmployee(string Name)
        {
            var employees = _permRepo.ReadAllPermanentEmployees();

            if (!employees.Exists(x => x.Name.Equals(Name))) { return null; }

            var employee = employees.Find(x => x.Name.Equals(Name));

            return employee;
        }

        public PermanentEmployee SearchJSONForPermanentEmployee(Guid id)
        {
            var employees = _permRepo.ReadAllPermanentEmployees();

            if (!employees.Exists(x => x.EmployeeID.Equals(id))) { return null; }

            var employee = employees.Find(x => x.EmployeeID.Equals(id));

            return employee;
        }              
    }
}
