using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IReadTemp
    {
        List<TempEmployee> ReadTempEmployee(string Name);
        List<TempEmployee> ReadAllTempEmployees();
    }
}
