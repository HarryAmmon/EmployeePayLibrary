using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IPermanentRepo: ICreatePermanent, IReadPermanent, IUpdatePermanent, IDeletePermanent
    {
        bool CheckPermanentEmployeeExists(string Name, out PermanentEmployee employee);
    }
}