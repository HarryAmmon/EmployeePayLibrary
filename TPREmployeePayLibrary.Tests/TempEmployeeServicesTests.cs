using log4net;
using Moq;
using TPREmployeePayLibrary.Services;
using Xunit;

namespace TPREmployeePaySolution.Tests
{
    public class TempEmployeeServicesTests
    {
        private static TempEmployeeServices _services;
        private readonly Mock<ILog> _log;

        public TempEmployeeServicesTests()
        {

            _log = new Mock<ILog>();
            _services = new TempEmployeeServices();
        }

        [Theory]
        [InlineData(56, 5, 1400)]
        [InlineData(10, 5.5, 275)]
        [InlineData(13, 0, 0)]
        public void Can_Calculate_Annual_Pay(decimal DailyRate, double WeeksWorked, decimal expectedAnnualPay)
        {
            // Arrange

            // Act
            var actualAnnualPay = _services.CalculateAnnualPay(DailyRate, WeeksWorked);
            // Assert
            Assert.Equal(expectedAnnualPay, actualAnnualPay, 2);

        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(49, 7)]
        [InlineData(23, 3.29)]
        public void Can_Calculate_Hourly_Pay(decimal DailyRate, decimal expectedHourlyPay)
        {
            // Arrange

            // Act
            var actualHourlyPay = _services.CalculateHourlyPay(DailyRate);

            // Assert
            Assert.Equal(expectedHourlyPay, actualHourlyPay, 2);
        }
    }
}
