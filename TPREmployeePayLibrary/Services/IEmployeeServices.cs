using System;

namespace TPREmployeePayLibrary.Services
{
    public interface IEmployeeServices
    {
        double CalcWeeksWorked(DateTimeOffset StartDate, DateTimeOffset EndDate);
    }
}
