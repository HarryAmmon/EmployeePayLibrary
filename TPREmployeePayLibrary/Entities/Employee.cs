using System;

namespace TPREmployeePayLibrary.Entities
{
    public abstract class Employee
    {
        public Guid EmployeeID { get; set; }
        public string Name { get; set; }

        public DateTimeOffset StartDate { get; private set; }

        public DateTimeOffset? EndDate { get; set; }

        public enum EmployeeType
        {
            Permanent,
            TempContractor
        };

        public EmployeeType Type { get; set; }

        public Employee(string Name)
        {
            EmployeeID = Guid.NewGuid();
            this.Name = Name;
            StartDate = DateTimeOffset.UtcNow.Date; // Might want to remove this functionality
        }

        public Employee(string Name, DateTimeOffset StartDate) : this(Name)
        {
            this.StartDate = StartDate;
        }
    }
}
