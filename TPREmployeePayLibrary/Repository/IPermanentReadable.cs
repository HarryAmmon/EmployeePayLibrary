using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentReadable
    {
        PermanentEmployee ReadPermanentEmployee(int id);

        List<PermanentEmployee> ReadAllPermanentEmployees();
        Task<PermanentEmployee> ReadPermanentEmployeeAsync(int id);
        Task<List<PermanentEmployee>> ReadAllPermanentEmployeesAsync();
    }
}