using log4net;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public class PermanentEmployeeServices : IPermanentEmployeeServices
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(PermanentEmployeeServices));

        public decimal CalculateAnnualPay(PermanentEmployee employee)
        {
            decimal annualPay = employee.AnnualSalary + employee.AnnualBonus;
            _log.Debug($"Calculated annual pay {annualPay}");
            _log.Info($"AnnualSalary: {employee.AnnualSalary}, AnnualBonus: {employee.AnnualBonus}");
            return annualPay;
        }

        public decimal CalculateHourlyPay(PermanentEmployee employee)
        {
            decimal hourlyPay = (((employee.AnnualSalary / 52) / 5) / 7);
            _log.Debug($"Calculated hourly pay {hourlyPay}");
            _log.Info($"AnnualSalary: {employee.AnnualSalary}");
            return hourlyPay;
        }
    }
}
