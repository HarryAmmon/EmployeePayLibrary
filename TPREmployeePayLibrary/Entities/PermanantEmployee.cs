using Newtonsoft.Json;
using System;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Entites
{
    public class PermanentEmployee : Employee
    {

        public decimal AnnualSalary { get; set; }
        public decimal AnnualBonus { get; set; }

        public PermanentEmployee(string Name) : base(Name)
        {
            this.Type = EmployeeType.Permanent;
        }

        [JsonConstructor]
        public PermanentEmployee(string Name, decimal AnnualSalary, decimal AnnualBonus, DateTimeOffset StartDate) : base(Name, StartDate)
        {
            if (AnnualSalary >= 0 && AnnualBonus >= 0)
            {
                this.AnnualBonus = AnnualBonus;
                this.AnnualSalary = AnnualSalary;
                this.Type = EmployeeType.Permanent;
            }
            else { throw new Exception("Annual Salary and Annual Bonus must be 0 or greater."); }

        }
    }
}
