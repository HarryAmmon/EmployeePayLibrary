using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempReadable
    {
        TempEmployee ReadTempEmployee(int id);
        List<TempEmployee> ReadAllTempEmployees();
    }
}
