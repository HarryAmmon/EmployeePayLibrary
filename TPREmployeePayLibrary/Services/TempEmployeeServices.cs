using log4net;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public class TempEmployeeServices : ITempEmployeeServices
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(TempEmployeeServices));
         
        public decimal CalculateAnnualPay(TempEmployee employee)
        {
            var weeksWorked = (decimal)employee.CalcWeeksWorked();
            var annualPay = (employee.DailyRate * 5 * weeksWorked);
            _log.Debug($"Calculated annualPay: {annualPay}");
            _log.Info($"DailyRate: {employee.DailyRate}, WeeksWorked:{employee.WeeksWorked}");
            return annualPay;
        }

        public decimal CalculateHourlyPay(TempEmployee employee)
        {
            if (employee.DailyRate > 0)
            {
                decimal hourlyPay = employee.DailyRate / 7;
                _log.Debug($"Calculated hourlyPay: {hourlyPay}");
                _log.Info($"DailyRate: {employee.DailyRate}");
                return hourlyPay;
            }
            else
            {
                _log.Error($"Can not calculate hourly rate if negative days have been worked. DailyRate: {employee.DailyRate}");
                return -1;
            }
        }
    }
}