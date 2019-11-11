using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentWriteable
    {
        PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee);

        bool UpdatePermanentEmployee(PermanentEmployee employee, string field, string value);

        bool DeletePermanentEmployee(PermanentEmployee employee);
    }
}