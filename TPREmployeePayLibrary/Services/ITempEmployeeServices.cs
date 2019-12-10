using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public interface ITempEmployeeServices
    {
        decimal CalculateAnnualPay(TempEmployee employee);

        decimal CalculateHourlyPay(TempEmployee employee);
    }
}
