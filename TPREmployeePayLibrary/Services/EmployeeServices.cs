using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPREmployeePayLibrary.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly ILog _Log = LogManager.GetLogger(typeof(EmployeeServices));

        public double CalcWeeksWorked(DateTimeOffset StartDate, DateTimeOffset EndDate)
        {
            _Log.Info($"Calculating WeeksWorked with Start Date and End Date");
            
            if(StartDate > DateTimeOffset.Now)
            {
                _Log.Info("Current Date is before start date.");
                _Log.Info("Calculation successful");
                return 0;
            }
            else if (StartDate <= EndDate)
            {
                var result = (EndDate - StartDate).TotalDays / 7;
                _Log.Info($"Calculation successful.");
                return result;
            }
            else
            {
                _Log.Error($"Unable to calculate weeks worked as the start date was the same or later than the end date! StartDate: {StartDate} EndDate: {EndDate}.");
                throw new Exception();
            }
        }

        public double CalcWeeksWorked(DateTimeOffset StartDate)
        {
            _Log.Info($"Calculating WeeksWorked with Start Date and Todays Date: {DateTimeOffset.Now}");
            if (StartDate > DateTimeOffset.Now)
            {
                _Log.Info("Current Date is before start date.");
                _Log.Info("Calculation successful");
                return 0;
            }
            else
            {
                var result = (DateTimeOffset.Now - StartDate).TotalDays / 7;
                _Log.Info($"Calculation successful.");
                return result;
            }
            
        }
    }
}
