using log4net;
using log4net.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TPREmployeePayLibrary.Services;
using Xunit;

namespace TPREmployeePaySolution.Tests
{
    public class TestData : IEnumerable<Object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
        new object[] {new DateTimeOffset(2018,5,4,0,0,0,TimeSpan.Zero), new DateTimeOffset(2019,5, 4, 0, 0, 0,TimeSpan.Zero), 52.14 },
        new object[] {new DateTimeOffset(2018,9, 2,0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2017,9,10, 0, 0, 0, TimeSpan.Zero), 0},
        new object[] {new DateTimeOffset(2019,10,8, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2020,12,24, 0, 0, 0, TimeSpan.Zero), 63.29}
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class EmployeeServicesTests
    {

        public EmployeeServices employeeServices;
        private readonly ILog log;
        public EmployeeServicesTests()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log = LogManager.GetLogger(typeof(EmployeeServicesTests));
            employeeServices = new EmployeeServices();
        }


        [Theory]
        [ClassData(typeof(TestData))]
        public void Can_Calculate_An_Employees_WeeksWorked(DateTimeOffset startDate, DateTimeOffset endDate, double expectedResult)
        {
            // Act
            var actualResult = employeeServices.CalcWeeksWorked(startDate, endDate);

            // Assert
            Assert.Equal(expectedResult, actualResult, 2);
        }
    }
}
