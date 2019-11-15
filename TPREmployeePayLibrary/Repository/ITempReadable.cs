using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempReadable
    {
        TempEmployee ReadTempEmployee(Guid id);
        List<TempEmployee> ReadAllTempEmployees();
    }
}
