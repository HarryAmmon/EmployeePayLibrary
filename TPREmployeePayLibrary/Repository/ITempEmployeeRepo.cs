using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempEmployeeRepo
    {
        TempEmployee CreateTempEmployee(TempEmployee employee);

        List<TempEmployee> ReadTempEmployee(string Name);

        List<TempEmployee> ReadAllTempEmployees();

        bool DeleteTempEmployee(TempEmployee employee);

        bool UpdateTempEmployee(TempEmployee employee, string field, string value);

        bool CheckTempEmployeeExists(string Name, out TempEmployee employee);
    }
}
