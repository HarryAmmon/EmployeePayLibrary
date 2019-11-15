using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempWriteable
    {

        TempEmployee CreateTempEmployee(TempEmployee employee);

        bool UpdateTempEmployee(Guid id, string field, string value);

        bool DeleteTempEmployee(Guid id);

        bool SaveChanges();

    }
}
