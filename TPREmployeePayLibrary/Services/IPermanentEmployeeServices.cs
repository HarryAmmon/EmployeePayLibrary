using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public interface IPermanentEmployeeServices
    {
        public decimal CalculateAnnualPay(PermanentEmployee employee);

        public decimal CalculateHourlyPay(PermanentEmployee employee);
    }
}