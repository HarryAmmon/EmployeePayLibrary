using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface IDeletePermanent
    {
        bool DeletePermanentEmployee(PermanentEmployee employee);
    }
}
