using System.Collections.Generic;

namespace TPREmployeePayLibrary.Services
{
    public interface ICommandServices
    {
        bool AddTemp(string Name, string DailyRate, string StartDate);

        bool AddPermanent(string Name, string AnnualSalary, string AnnualBonus, string StartDate);

        IEnumerable<string> ViewAll();

        IEnumerable<string> ViewTemp();

        IEnumerable<string> ViewPermanent();

        IEnumerable<string> View(string Name);

        bool UpdateSalary(string Name, string field, string value);

        bool UpdateBonus(string Name, string field, string value);

        bool UpdateDailyRate(string Name, string field, string value);

        bool UpdateEndDate(string Name, string field, string value);

        bool Delete(string Name, string Type);
    }
}
