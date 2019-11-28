using TPREmployeePayLibrary.Entities;
using System;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentWriteable
    {
        PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee);

        bool DeletePermanentEmployee(Guid id);

        bool UpdatePermanentEmployee(PermanentEmployee employee);

        bool SaveChanges();
    }
}