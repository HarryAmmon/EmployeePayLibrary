using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempWriteable
    {

        TempEmployee CreateTempEmployee(TempEmployee employee);

        bool UpdateTempEmployee(TempEmployee employee, string field, string value);

        bool DeleteTempEmployee(TempEmployee employee);

    }
}
