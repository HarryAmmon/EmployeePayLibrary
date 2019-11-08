using Newtonsoft.Json;
using System;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Entites
{
    public class TempEmployee : Employee
    {
        public decimal DailyRate { get; set; }

        public TempEmployee(string Name) : base(Name)
        {
            Type = EmployeeType.TempContractor;
        }

        [JsonConstructor]
        public TempEmployee(string Name, decimal DailyRate, DateTimeOffset StartDate) : base(Name, StartDate)
        {
            if (DailyRate >= 0)
            {
                this.DailyRate = DailyRate;
                Type = EmployeeType.TempContractor;
            }
            else { throw new Exception("Daily Rate & Weeks Worked must be 0 or greater. "); }

        }
    }
}
