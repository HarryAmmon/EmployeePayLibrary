using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentReadable
    {
        List<PermanentEmployee> ReadPermanentEmployee(string Name);

        List<PermanentEmployee> ReadAllPermanentEmployees();
    }
}