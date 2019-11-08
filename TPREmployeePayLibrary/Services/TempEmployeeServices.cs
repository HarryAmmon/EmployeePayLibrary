using log4net;

namespace TPREmployeePayLibrary.Services
{
    public class TempEmployeeServices : ITempEmployeeServices
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(TempEmployeeServices));

        public TempEmployeeServices()
        {
        }

        public decimal CalculateAnnualPay(decimal DailyRate, double WeeksWorked)
        {
            decimal weeksWorked = (decimal)WeeksWorked;
            decimal annualPay = (DailyRate * 5 * weeksWorked);
            _log.Debug($"Calculated annualPay: {annualPay}");
            _log.Info($"DailyRate: {DailyRate}, WeeksWorked:{WeeksWorked}");
            return annualPay;
        }

        public decimal CalculateHourlyPay(decimal DailyRate)
        {
            if (DailyRate > 0)
            {
                decimal hourlyPay = DailyRate / 7;
                _log.Debug($"Calculated hourlyPay: {hourlyPay}");
                _log.Info($"DailyRate: {DailyRate}");
                return hourlyPay;
            }
            else
            {
                _log.Error($"Can not calculate hourly rate if negative days have been worked. DailyRate: {DailyRate}");
                return -1;
            }
        }
    }
}