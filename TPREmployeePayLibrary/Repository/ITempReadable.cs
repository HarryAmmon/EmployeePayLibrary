using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempReadable
    {
        TempEmployee ReadTempEmployee(Guid id);
        List<TempEmployee> ReadAllTempEmployees();
    }
}
