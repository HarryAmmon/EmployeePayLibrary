using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public static class PermanentEmployeeServicesWrapper
    {
        public static IPermanentEmployeeServices DefaultImplementation = new PermanentEmployeeServices();
        public static IPermanentEmployeeServices Implementation { private get; set; } = DefaultImplementation;

        public static void RevertToDefault()
        {
            Implementation = DefaultImplementation;
        }

        public static decimal CalculateAnnualPay(this PermanentEmployee employee)
        {
            return Implementation.CalculateAnnualPay(employee);
        }

        public static decimal CalculateHourlyPay(this PermanentEmployee employee)
        {
            return Implementation.CalculateHourlyPay(employee);
        }

    }
}
