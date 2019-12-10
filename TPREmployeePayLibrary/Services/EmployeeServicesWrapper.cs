using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Services
{
    public static class EmployeeServicesWrapper
    {
        private static IEmployeeServices DefaultImplementation = new EmployeeServices();
        public static IEmployeeServices Implementation { private get; set; } = DefaultImplementation;

        public static void RevertToDefault()
        {
            Implementation = DefaultImplementation;
        }

        public static double CalcWeeksWorked(this Employee employee)
        {
            return Implementation.CalcWeeksWorked(employee);
        }
    }
}
