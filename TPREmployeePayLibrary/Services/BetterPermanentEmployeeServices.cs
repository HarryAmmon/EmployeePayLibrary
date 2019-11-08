using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPREmployeePayLibrary.Services
{
    public class BetterPermanentEmployeeServices: PermanentEmployeeServices
    {
        public override decimal CalculateHourlyPay(decimal AnnualSalary)
        {
            decimal hourlyPay = (((AnnualSalary / 52) / 5) / 8.5m);
            _log.Debug($"Calculated hourly pay {hourlyPay}");
            _log.Info($"AnnualSalary: {AnnualSalary}");
            return hourlyPay;
        }
    }
}
