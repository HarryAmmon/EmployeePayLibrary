using TPREmployeePayLibrary.Entites;
using System;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentWriteable
    {
        PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee);

        bool UpdatePermanentEmployee(Guid id, string field, string value);

        bool DeletePermanentEmployee(Guid id);

        bool SaveChanges();
    }
}