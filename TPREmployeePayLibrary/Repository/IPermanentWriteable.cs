using TPREmployeePayLibrary.Entities;
using System;
using System.Threading.Tasks;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentWriteable
    {
        PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee);

        bool DeletePermanentEmployee(int id);

        bool UpdatePermanentEmployee(PermanentEmployee employee);

        Task<PermanentEmployee> CreatePermanentEmployeeAsync(PermanentEmployee employee);

        Task<bool> DeletePermanentEmployeeAsync(int id);

        Task<PermanentEmployee> UpdatePermanentEmployeeAsync(PermanentEmployee employee);
    }
}