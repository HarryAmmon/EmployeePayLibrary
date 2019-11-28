using System;
using System.Collections.Generic;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentReadable
    {
        PermanentEmployee ReadPermanentEmployee(Guid id);

        List<PermanentEmployee> ReadAllPermanentEmployees();
    }
}