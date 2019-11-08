using log4net;
using Moq;
using TPREmployeePayLibrary.Services;
using Xunit;

namespace TPREmployeePaySolution.Tests
{
    public class PermanentEmployeeServicesTests
    {
        PermanentEmployeeServices _services;
        private readonly Mock<ILog> _log;
        public PermanentEmployeeServicesTests()
        {
            _log = new Mock<ILog>();
            _services = new PermanentEmployeeServices();
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(10, 10, 20)]
        [InlineData(100000, 1, 100001)]
        public void Can_Calculate_Annual_Pay(decimal AnnualSalary, decimal AnnualBonus, decimal AnnualPay)
        {
            // arrange

            // act
            var actualResult = _services.CalculateAnnualPay(AnnualSalary, AnnualBonus);
            // assert
            Assert.Equal(AnnualPay, actualResult);
        }

        [Theory]
        [InlineData(100, 0.054)]
        [InlineData(99999, 54.94)]
        [InlineData(0, 0)]
        public void Can_Calculate_Hourly_Pay(decimal AnnualSalary, decimal HourlyPay)
        {
            // arrange

            // act
            var actualResult = _services.CalculateHourlyPay(AnnualSalary);

            // assert
            Assert.Equal(HourlyPay, actualResult, 2);
        }

    }
}
