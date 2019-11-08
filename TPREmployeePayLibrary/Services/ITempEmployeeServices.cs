namespace TPREmployeePayLibrary.Services
{
    public interface ITempEmployeeServices
    {
        decimal CalculateAnnualPay(decimal HourlyRate, double WeeksWorked);

        decimal CalculateHourlyPay(decimal HourlyRate);
    }
}
