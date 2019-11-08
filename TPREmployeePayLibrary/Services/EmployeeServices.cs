using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPREmployeePayLibrary.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly ILog _Log = LogManager.GetLogger(typeof(EmployeeServices));

        public EmployeeServices()
        {
        }

        public double CalcWeeksWorked(DateTimeOffset StartDate, DateTimeOffset EndDate)
        {
            double weeksWorked = 0;
            _Log.Info($"Calculating WeeksWorked");
            if (EndDate > StartDate) { weeksWorked = (EndDate - StartDate).TotalDays / 7; }
            else { _Log.Error($"Unable to calculate weeks worked as the start date was the same or later than the end date! StartDate: {StartDate} EndDate: {EndDate}."); }
            return weeksWorked;
        }
    }
}
