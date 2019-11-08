using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ICreateTemp
    {
        TempEmployee CreateTempEmployee(TempEmployee employee);
    }
}
