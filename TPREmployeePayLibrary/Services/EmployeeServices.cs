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

        public double CalcWeeksWorked(DateTimeOffset StartDate, DateTimeOffset? EndDate)
        {
            if (EndDate == null) { EndDate = DateTimeOffset.UtcNow.Date; }
            double weeksWorked;
            _Log.Info($"Calculating WeeksWorked");
            if (StartDate >= EndDate)
            {
                weeksWorked = (StartDate - EndDate).Value.Days / 7;
            }
            else
            {
                weeksWorked = -1;
                _Log.Error($"Unable to calculate weeks worked as the start date was the same or later than the end date! StartDate: {StartDate} EndDate: {EndDate}.");
                throw new Exception();
            }
            return weeksWorked;
        }
    }
}
