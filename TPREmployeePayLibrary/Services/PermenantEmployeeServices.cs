using log4net;

namespace TPREmployeePayLibrary.Services
{
    public class PermanentEmployeeServices : IPermanentEmployeeServices
    {
        protected readonly ILog _log = LogManager.GetLogger(typeof(PermanentEmployeeServices));
        public PermanentEmployeeServices()
        {
        }
        public decimal CalculateAnnualPay(decimal AnnualSalary, decimal AnnualBonus)
        {
            decimal annualPay = AnnualSalary + AnnualBonus;
            _log.Debug($"Calculated annual pay {annualPay}");
            _log.Info($"AnnualSalary: {AnnualSalary}, AnnualBonus: {AnnualBonus}");
            return annualPay;
        }

        public virtual decimal CalculateHourlyPay(decimal AnnualSalary)
        {
            decimal hourlyPay = (((AnnualSalary / 52) / 5) / 7);
            _log.Debug($"Calculated hourly pay {hourlyPay}");
            _log.Info($"AnnualSalary: {AnnualSalary}");
            return hourlyPay;
        }
    }
}
