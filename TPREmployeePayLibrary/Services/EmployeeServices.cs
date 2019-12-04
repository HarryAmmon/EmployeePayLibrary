using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public static class EmployeeServices
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(EmployeeServices));

        public static double CalcWeeksWorked(this Employee employee)
        {
            _Log.Info($"Calculating WeeksWorked for employee: {employee.EmployeeID}.");
            
            if(employee.StartDate > DateTimeOffset.Now)
            {
                _Log.Info("Current Date is before start date.");
                _Log.Info("Calculation successful");
                return 0;
            }
            else if (employee.EndDate == DateTimeOffset.MinValue)
            {
                var result = (DateTimeOffset.Now - employee.StartDate).TotalDays / 7;
                _Log.Info($"Using the current date for calculation.");
                _Log.Info($"Calculation successful.");
                return result;
            }
            else
            {
                var result = (employee.EndDate - employee.StartDate).TotalDays / 7;
                _Log.Info($"Calculation successful");
                return result;
            }
        }
    }
}
