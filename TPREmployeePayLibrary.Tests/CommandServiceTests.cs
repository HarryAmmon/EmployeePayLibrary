
using log4net;
using Moq; // Mocking 
using System;
using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;
using TPREmployeePayLibrary.Repository;
using TPREmployeePayLibrary.Services;
using Xunit;

namespace TPREmployeePaySolution.Tests
{
    public class CommandServiceTests
    {
        private readonly List<PermanentEmployee> _permEmployees;
        private readonly List<TempEmployee> _tempEmployees;

        private readonly ICommandServices _services;


        private readonly Mock<IPermanentRepo> _repoPermanent;
        private readonly Mock<ITempEmployeeRepo> _repoTemp;

        private readonly Mock<PermanentEmployee> _permEmployee;

        private readonly Mock<IPermanentEmployeeServices> _permServices;
        private readonly Mock<ITempEmployeeServices> _tempServices;
        private readonly Mock<IEmployeeServices> _empServices;

        private readonly Mock<ILog> _log;

        // private readonly ITestOutputHelper output; // DONT REMOVE ME I exist for learning purposes. XUnit removes the functionality of "Console.Writeline()" due to tests running in parrallel. 
        // If an ouput is required this interface should be passed to the test constructor as shown below
        // Instead of using "Console" use "output". The output can be viewed as part of the "open additional output..." section of the test in test exploerer in VS.
        // This option will only show if "output" is used.

        public CommandServiceTests(/*ITestOutputHelper outputDont remove pls*/)
        {
            _permEmployee = new Mock<PermanentEmployee>();

            _repoPermanent = new Mock<IPermanentRepo>();
            _repoTemp = new Mock<ITempEmployeeRepo>();

            _permServices = new Mock<IPermanentEmployeeServices>();
            _tempServices = new Mock<ITempEmployeeServices>();
            _empServices = new Mock<IEmployeeServices>();

            _log = new Mock<ILog>();

            _services = new BetterCommandServices(_repoTemp.Object, _repoPermanent.Object, _empServices.Object, _permServices.Object, _tempServices.Object);
            //this.output = output; // DONT REMOVE, learning purposes 
            _permEmployees = new List<PermanentEmployee>() { new PermanentEmployee("qwerty1", 100000, 20000, new DateTimeOffset(2019, 4, 2, 0, 0, 0, TimeSpan.Zero)) };     // Create a list of Permanent employees
            _tempEmployees = new List<TempEmployee>() { new TempEmployee("qwerty1", 39, new DateTimeOffset(2019, 6, 30, 0, 0, 0, TimeSpan.Zero)) };                         // Create a list of temp employees
        }

        [Fact]
        public void Can_Run_View_All_Command()
        {
            //Arrange
            View_All_Setup();

            // Act
            var results = _services.ViewAll();                                                    // Calls the command to be tested
            var resultCount = 0;
            foreach (var result in results)
            {
                resultCount++;
            }
            // Assert
            Assert.Equal(2, resultCount);
        }

        [Fact]
        public void View_All_Command_Uses_EmployeeRepositoryStatic_ReadAllPermanentEmployee_Method()
        {
            // Arrange
            View_All_Setup();
            // Act
            _services.ViewAll();
            // Assert
            _repoPermanent.Verify(x => x.ReadAllPermanentEmployees());                                                                       // Ensures that the ReadAllPermanentEmployee method has been called at least once
        }
        [Fact]
        public void View_All_Command_Uses_EmployeeRepositoryStatic_ReadAllTempEmployee_Method()
        {
            // Arrange
            View_All_Setup();
            // Act
            _services.ViewAll();
            // Assert
            _repoTemp.Verify(x => x.ReadAllTempEmployees());                                                                            // Ensures that the ReadAllTempEmployee method has been called at least once
        }
        [Fact]
        public void View_All_Command_Uses_TempServices_CalculateHourlyPay_Method()
        {
            // Arrange
            View_All_Setup();
            // Act
            _services.ViewAll();
            // Assert
            _tempServices.Verify(x => x.CalculateHourlyPay(It.IsAny<decimal>()));                                                   // Enusres that the CalculateHourlyPay method has been called at least once
        }
        [Fact]
        public void View_All_Command_Uses_TempServices_CalculateAnnualPay_Method()
        {
            // Arrange
            View_All_Setup();
            // Act
            _services.ViewAll();
            // Assert
            _tempServices.Verify(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<double>()));                              // Ensures that the CalculateAnnualPay method has been called at least once
        }
        [Fact]
        public void View_All_Command_Uses_PermanentServices_CalculteHourlyPay_Method()
        {
            // Arrange
            View_All_Setup();
            // Act
            _services.ViewAll();
            // Assert
            _permServices.Verify(x => x.CalculateHourlyPay(It.IsAny<decimal>()));                                                  // Ensures that the CalculateHourlyPay method has been called at least once
        }
        [Fact]
        public void View_All_Command_Uses_PermanentServices_CalculateAnnualPay_Method()
        {
            // Arrange
            View_All_Setup();
            // Act
            _services.ViewAll();
            // Assert
            _permServices.Verify(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>()));                              // Ensures that the CalculateAnnualPay method has been called at least once
        }

        [Fact]
        public void Can_Run_View_Temp_Command()
        {
            // Arrange
            ViewTempSetup();
            // Act
            var results = _services.ViewTemp();
            // Assert
            Assert.Single(results);
        }

        [Fact]
        public void View_Temp_Command_Uses_EmployeeRepositoryStatic_ReadAllTempEmployees()
        {
            // Arrange
            ViewTempSetup();
            // Act
            var results = _services.ViewTemp();
            // Assert
            _repoTemp.Verify(x => x.ReadAllTempEmployees());
        }
        [Fact]
        public void View_Temp_Command_Uses_TempServices_CalculateHourlyPay()
        {
            // Arrange
            ViewTempSetup();
            // Act
            var results = _services.ViewTemp();
            // Assert
            _tempServices.Verify(x => x.CalculateHourlyPay(It.IsAny<decimal>()));
        }
        [Fact]
        public void View_Temp_Command_Uses_TempServices_CalculateAnnualPay()
        {
            // Arrange
            ViewTempSetup();
            // Act
            var results = _services.ViewTemp();
            // Assert
            _tempServices.Verify(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<double>()));
        }
        [Fact]
        public void Can_Run_View_Permanent_Command()
        {
            // Arrange
            ViewPermanentSetup();

            // Act
            var results = _services.ViewPermanent();

            // Assert           
            Assert.Single(results);

        }

        [Fact]
        public void View_Permanent_Command_Uses_EmployeeRepositoryStatic_ReadAllPermanentEmployees()
        {
            // Arrange
            ViewPermanentSetup();

            // Act
            var results = _services.ViewPermanent();

            // Assert
            _repoPermanent.Verify(x => x.ReadAllPermanentEmployees());
        }

        [Fact]
        public void View_Permanent_Command_Uses_PermanentEmployeeServices_CalculateAnnualPay()
        {
            // Arrange
            ViewPermanentSetup();

            // Act
            var results = _services.ViewPermanent();

            // Assert
            _permServices.Verify(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>()));
        }
        [Fact]
        public void View_Permanent_Command_Uses_PermanentEmployeeServices_CalculateHourlyPay()
        {
            // Arrange
            ViewPermanentSetup();

            // Act
            var results = _services.ViewPermanent();

            // Assert
            _permServices.Verify(x => x.CalculateHourlyPay(It.IsAny<decimal>()));
        }

        [Fact]
        public void Can_Run_View_Name_Command()
        {
            // Arrange
            ViewNameSetup();

            // Act
            var results = _services.View("qwerty");

            // Assert
            foreach (var result in results)
            {
                Assert.Contains("qwerty", result);
            }
        }

        [Fact]
        public void View_Name_Command_Runs_EmployeeRepositoryStatic_ReadPermanentEmployee()
        {
            // Arrange
            ViewNameSetup();

            // Act
            var results = _services.View("qwerty1");
            // Assert
            _repoPermanent.Verify(x => x.ReadPermanentEmployee(It.IsAny<string>()));

        }

        [Fact]
        public void View_Name_Command_Runs_EmployeeRepositoryStatic_ReadTempEmployee()
        {
            // Arrange
            ViewNameSetup();

            // Act
            var results = _services.View("qwerty1");
            // Assert
            _repoTemp.Verify(x => x.ReadTempEmployee(It.IsAny<string>()));
        }

        [Fact]
        public void View_Name_Command_Runs_PermServices_CalculateAnnualPay()
        {
            // Arrange
            ViewNameSetup();

            // Act
            var results = _services.View("qwerty1");
            // Assert
            _permServices.Verify(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>()));
        }

        [Fact]
        public void View_Name_Command_Runs_TempServices_CalculateAnnualPay()
        {
            // Arrange
            ViewNameSetup();

            // Act
            var results = _services.View("qwerty1");
            // Assert
            _tempServices.Verify(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<double>()));
        }
        [Fact]
        public void View_Name_Command_Runs_PermServices_CalculateHourlyPay()
        {
            // Arrange
            ViewNameSetup();

            // Act
            var results = _services.View("qwerty1");
            // Assert
            _permServices.Verify(x => x.CalculateHourlyPay(It.IsAny<decimal>()));
        }
        [Fact]
        public void View_Name_Command_Runs_TempServices_CalculateHourlyPay()
        {
            // Arrange
            ViewNameSetup();
            // Act
            var results = _services.View("qwerty1");
            // Assert
            _tempServices.Verify(x => x.CalculateHourlyPay(It.IsAny<decimal>()));
        }

        [Fact]
        public void Delete_Command_Fails_Given_Invalid_Arguments()
        {
            // Act
            var result = _services.Delete("sdfa", "contractor");

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void Can_Run_Delete_Command_For_PermanentEmployees()
        {
            var permEmployee = new PermanentEmployee("aer");
            // Arrange 
            _repoPermanent.Setup(x => x.DeletePermanentEmployee(It.IsAny<PermanentEmployee>()))
                .Returns(true);
            _repoPermanent.Setup(x => x.CheckPermanentEmployeeExists(It.IsAny<string>(), out permEmployee))
                .Returns(true);
            _repoPermanent.Setup(x => x.ReadPermanentEmployee(It.IsAny<string>()))
                .Returns(new List<PermanentEmployee>() { new PermanentEmployee("") });
            _repoPermanent.Setup(x => x.DeletePermanentEmployee(It.IsAny<PermanentEmployee>()))
                .Returns(true);

            // Act
            var result = _services.Delete("Harry", "permanent");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Can_Run_Delete_Command_For_TempEmployees()
        {
            var tempEmployee = new TempEmployee("aer");
            // Arrange
            _repoTemp.Setup(x => x.DeleteTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(true);
            _repoTemp.Setup(x => x.CheckTempEmployeeExists(It.IsAny<string>(), out tempEmployee))
               .Returns(true);
            _repoTemp.Setup(x => x.ReadTempEmployee(It.IsAny<string>()))
                .Returns(new List<TempEmployee>() { new TempEmployee("") });
            _repoTemp.Setup(x => x.DeleteTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(true);

            // Act
            var result = _services.Delete("qwerty", "temp");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_Permanent_Command_Runs_EmployeeRepositoryStatic_DeletePermanentEmployee()
        {
            var permEmployee = new PermanentEmployee("aer");
            _repoPermanent.Setup(x => x.DeletePermanentEmployee(It.IsAny<PermanentEmployee>()))
                .Returns(true);
            _repoPermanent.Setup(x => x.CheckPermanentEmployeeExists(It.IsAny<string>(), out permEmployee))
                .Returns(true);
            _repoPermanent.Setup(x => x.ReadPermanentEmployee(It.IsAny<string>()))
                .Returns(new List<PermanentEmployee>() { new PermanentEmployee("") });
            _repoPermanent.Setup(x => x.DeletePermanentEmployee(It.IsAny<PermanentEmployee>()))
                .Returns(true);

            // Act
            var result = _services.Delete("qwerty", "permanent");

            // Assert
            _repoPermanent.Verify(x => x.DeletePermanentEmployee(It.IsAny<PermanentEmployee>()));
        }

        [Fact]
        public void Delete_Temp_Command_Runs_EmployeeRepositoryStatic_DeleteTempEmployee()
        {
            var tempEmployee = new TempEmployee("1wasd");
            _repoTemp.Setup(x => x.DeleteTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(true);
            _repoTemp.Setup(x => x.CheckTempEmployeeExists(It.IsAny<string>(), out tempEmployee))
               .Returns(true);
            _repoTemp.Setup(x => x.ReadTempEmployee(It.IsAny<string>()))
                .Returns(new List<TempEmployee>() { new TempEmployee("") });
            _repoTemp.Setup(x => x.DeleteTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(true);

            // Act
            var result = _services.Delete("qwerty", "temp");

            // Assert
            _repoTemp.Verify(x => x.DeleteTempEmployee(It.IsAny<TempEmployee>()));
        }

        [Fact]
        public void Add_Temp_Employee_With_Name_DailyRate_StartDate()
        {
            // Arrange
            _repoTemp.Setup(x => x.CreateTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(_tempEmployees[0]);

            // Act
            var result = _services.AddTemp("Harry", "100", "2019-8-10");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_Temp_Employee_With_Name_DailyRate_Uses_CreateTempEmployee()
        {
            // Arrange
            _repoTemp.Setup(x => x.CreateTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(_tempEmployees[0]);

            // Act
            var result = _services.AddTemp("Harry", "100", "2019-5-2");

            // Assert
            _repoTemp.Verify(x => x.CreateTempEmployee(It.IsAny<TempEmployee>()));
        }

        [Fact]
        public void Add_Temp_Employee_Calls_CreateTempEmployee()
        {
            // Arrange
            _repoTemp.Setup(x => x.CreateTempEmployee(It.IsAny<TempEmployee>()))
                .Returns(_tempEmployees[0]);

            // Act
            var result = _services.AddTemp("Harry", "100", "2019-5-1");

            // Assert
            _repoTemp.Verify(x => x.CreateTempEmployee(It.IsAny<TempEmployee>()));
        }



        [Fact]
        public void Add_Permanent_Employee_With_Name_AnnualSalary_AnnualBonus()
        {
            // Arrange
            _repoPermanent.Setup(x => x.CreatePermanentEmployee(It.IsAny<PermanentEmployee>()))
                .Returns(_permEmployees[0]);

            // Act
            var result = _services.AddPermanent("Harry", "100", "0", "2017-8-5");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_Permanent_Employee_Calls_CreatePermanentEmployee_With_Four_Parameters()
        {
            // Arrange
            _repoPermanent.Setup(x => x.CreatePermanentEmployee(It.IsAny<PermanentEmployee>()))
                .Returns(_permEmployees[0]);

            // Act
            var result = _services.AddPermanent("Harry", "100", "0", "2019-10-11");

            // Assert
            _repoPermanent.Verify(x => x.CreatePermanentEmployee(It.IsAny<PermanentEmployee>()));
        }

        
        /*
         * Setup Methods
         */

        private void View_All_Setup()
        {
            _repoPermanent.Setup(x => x.ReadAllPermanentEmployees())                                                                         // Creates a mock of ReadAllPermanentEmployee
                .Returns(_permEmployees);                                                                                           // Tells the mock to return the list of _permEmployees
            _repoTemp.Setup(x => x.ReadAllTempEmployees())                                                                              // Creates a mock of ReadAllTempEmployees
                .Returns(_tempEmployees);                                                                                           // Tells the mock to return the list of _tempEmployees

            _tempServices.Setup(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<double>()))                                // Creates a mock of CalculateAnnualPay for tempEmployee
                .Returns(10);                                                                                                       // Tells the mock to return 10
            _tempServices.Setup(x => x.CalculateHourlyPay(It.IsAny<decimal>()))                                                     // Creates a mock of CalculateHourlyPay for tempEmployee
                .Returns(10);                                                                                                       // Tells the mock to return 10

            _permServices.Setup(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>()))                                // Creates a mock of CalculateAnnualPay for permEmployee
                .Returns(10);                                                                                                       // Tells the mock to return 10
            _permServices.Setup(x => x.CalculateHourlyPay(It.IsAny<decimal>()))                                                     // Creates a mock of CalculateHourlyPay for permEmployee
                .Returns(10);                                                                                                       // Tells the mock to return 10
        }

        private void ViewTempSetup()
        {
            _repoTemp.Setup(x => x.ReadAllTempEmployees()).Returns(_tempEmployees);
            _tempServices.Setup(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<double>())).Returns(9001);
            _tempServices.Setup(x => x.CalculateHourlyPay(It.IsAny<decimal>())).Returns(72);
        }

        private void ViewPermanentSetup()
        {
            _repoPermanent.Setup(x => x.ReadAllPermanentEmployees()).Returns(_permEmployees);
            _permServices.Setup(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(2010);
            _permServices.Setup(x => x.CalculateHourlyPay(It.IsAny<decimal>())).Returns(2);
        }

        private void ViewNameSetup()
        {
            _repoPermanent.Setup(x => x.ReadPermanentEmployee(It.IsAny<string>()))
                .Returns(_permEmployees.FindAll(x => x.Name.Equals("qwerty1")));
            _repoTemp.Setup(x => x.ReadTempEmployee(It.IsAny<string>()))
                .Returns(_tempEmployees.FindAll(x => x.Name.Equals("qwerty1")));

            _permServices.Setup(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(2010);
            _permServices.Setup(x => x.CalculateHourlyPay(It.IsAny<decimal>())).Returns(2);

            _tempServices.Setup(x => x.CalculateAnnualPay(It.IsAny<decimal>(), It.IsAny<double>())).Returns(9000);
            _tempServices.Setup(x => x.CalculateHourlyPay(It.IsAny<decimal>())).Returns(5);
        }

    }
}
