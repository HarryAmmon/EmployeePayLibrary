using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public static class TempEmployeeServicesWrapper
    {
        public static ITempEmployeeServices DefaultImplementation = new TempEmployeeServices();
        public static ITempEmployeeServices Implementaion { private get; set; } = DefaultImplementation;

        public static void RevertToDefault()
        {
            Implementaion = DefaultImplementation;
        }

        public static decimal CalculateAnnualPay(this TempEmployee employee)
        {
            return Implementaion.CalculateAnnualPay(employee);
        }

        public static decimal CalculateHourlyPay(this TempEmployee employee)
        {
            return Implementaion.CalculateHourlyPay(employee);
        }
    }
}
