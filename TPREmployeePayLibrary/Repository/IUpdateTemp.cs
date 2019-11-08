using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IUpdateTemp
    {
        bool UpdateTempEmployee(TempEmployee employee, string field, string value);
    }
}
