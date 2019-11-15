using System;
using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentReadable
    {
        PermanentEmployee ReadPermanentEmployee(Guid id);

        List<PermanentEmployee> ReadAllPermanentEmployees();
    }
}