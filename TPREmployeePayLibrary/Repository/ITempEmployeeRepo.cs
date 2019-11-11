using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempEmployeeRepo: ITempReadable, ITempWriteable
    {   
        bool CheckTempEmployeeExists(string Name, out TempEmployee employee);
    }
}
