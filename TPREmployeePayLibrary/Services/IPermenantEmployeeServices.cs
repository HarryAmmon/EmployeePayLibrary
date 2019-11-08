namespace TPREmployeePayLibrary.Services
{
    public interface IPermanentEmployeeServices
    {
        decimal CalculateAnnualPay(decimal AnnualSalary, decimal AnnualBonus);

        decimal CalculateHourlyPay(decimal AnnualSalary);
    }
}
