using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public interface ITempEmployeeRepo: ICreateTemp, IReadTemp, IUpdateTemp, IDeleteTemp
    {   
        bool CheckTempEmployeeExists(string Name, out TempEmployee employee);
    }
}
