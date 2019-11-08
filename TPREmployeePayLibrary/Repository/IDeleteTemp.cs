using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IDeleteTemp
    {
        bool DeleteTempEmployee(TempEmployee employee);
    }
}
