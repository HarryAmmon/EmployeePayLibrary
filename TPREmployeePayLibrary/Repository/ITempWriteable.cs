using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempWriteable
    {

        TempEmployee CreateTempEmployee(TempEmployee employee);

        bool UpdateTempEmployee(TempEmployee employee);

        bool DeleteTempEmployee(int id);
        
        bool SaveChanges();

    }
}
