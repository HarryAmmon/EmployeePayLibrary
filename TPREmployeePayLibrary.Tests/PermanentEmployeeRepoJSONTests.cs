using log4net;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TPREmployeePayLibrary.Entites;
using TPREmployeePayLibrary.Repository;
using Xunit;

namespace TPREmployeePaySolution.Tests
{
    public class PermanentEmployeeRepoJSONTests
    {
        private readonly PermanentEmployeeRepositoryJSON repo;
        private readonly Mock<ILog> _log;
        private readonly string JSONPath = @"..\employeeData\permanentEmployee.json";

        public PermanentEmployeeRepoJSONTests()
        {
            _log = new Mock<ILog>();
            repo = new PermanentEmployeeRepositoryJSON();
            LoadTestData();
        }

        [Fact]
        public void Can_Add_New_Permanent_Employee()
        {
            // Arrange
            var employee = new PermanentEmployee("Harry Smith", 20000, 111, new DateTimeOffset(2019, 6, 2, 0, 0, 0, TimeSpan.Zero));
            var expectedResult = employee.EmployeeID;

            // Act
            repo.CreatePermanentEmployee(employee);
            var actualResult = SearchJSONForPermanentEmployee(employee.EmployeeID).EmployeeID;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Can_Delete_Permanent_Employee()
        {
            // Arrange
            var employeeToDelete = SearchJSONForPermanentEmployee("PermanentPerson101")[0];


            // Act
            repo.DeletePermanentEmployee(employeeToDelete);
            var expectedResult = SearchJSONForPermanentEmployee(employeeToDelete.EmployeeID);
            // 
            Assert.Null(expectedResult);
        }

        [Fact]
        public void Can_Read_All_PermanentEmployees()
        {
            // Arrange

            // Act
            var list = repo.ReadAllPermanentEmployees();

            // Assert
            Assert.NotNull(list);
        }

        [Fact]
        public void Can_Read_Permanent_Employee_Based_On_Name()
        {
            // Arrange
            var employee = SearchJSONForPermanentEmployee("PermanentPerson100")[0];

            // Act
            var actualResult = repo.ReadPermanentEmployee(employee.Name)[0];

            // Assert
            Assert.Equal(employee.EmployeeID, actualResult.EmployeeID);
        }

        [Fact]
        public void Can_Update_PermanentEmployee_Name()
        {
            // Arrange
            var employeeToUpdate = SearchJSONForPermanentEmployee("PermanentPerson1001")[0];
            var employeeID = employeeToUpdate.EmployeeID;
            var expectedName = "Jeffery";

            // Act
            repo.UpdatePermanentEmployee(employeeToUpdate, "name", expectedName);
            var actualName = SearchJSONForPermanentEmployee(employeeID).Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Can_Update_PermanentEmployee_AnnualSalary()
        {
            // Arrange
            var employeeToUpdate = SearchJSONForPermanentEmployee("PermanentPerson1000")[0];
            var employeeID = employeeToUpdate.EmployeeID;
            var expectedSalary = "999666";

            // Act
            repo.UpdatePermanentEmployee(employeeToUpdate, "salary", expectedSalary);
            var actualSalary = SearchJSONForPermanentEmployee(employeeID).AnnualSalary;

            // Assert
            Assert.Equal(decimal.Parse(expectedSalary), actualSalary, 2);
        }

        [Fact]
        public void Can_Update_PermanentEmployee_AnnualBonus()
        {
            // Arrange
            var employeeToUpdate = SearchJSONForPermanentEmployee("PermanentPerson11")[0];
            var employeeID = employeeToUpdate.EmployeeID;
            var expectedBonus = "40172";

            // Act
            repo.UpdatePermanentEmployee(employeeToUpdate, "bonus", expectedBonus);
            var actualBonus = SearchJSONForPermanentEmployee(employeeID).AnnualBonus;

            // Assert
            Assert.Equal(decimal.Parse(expectedBonus), actualBonus, 2);
        }

        private List<PermanentEmployee> SearchJSONForPermanentEmployee(string Name)
        {
            string permanentJSONPath = JSONPath;

            // Check if file exists
            var fileInfo = new FileInfo(permanentJSONPath);
            var path = fileInfo.FullName;
            List<PermanentEmployee> permanentEmployees;
            if (!fileInfo.Exists)
            {
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }

            // Read the file 
            using (var file = File.OpenText(permanentJSONPath))
            {
                var serializer = new JsonSerializer();
                permanentEmployees = (List<PermanentEmployee>)serializer.Deserialize(file, typeof(List<PermanentEmployee>)) ?? new List<PermanentEmployee>();
            }

            // Search for employee based on id
            var employee = permanentEmployees.FindAll(x => x.Name.Equals(Name));
            return employee;
        }

        private PermanentEmployee SearchJSONForPermanentEmployee(Guid id)
        {
            string permanentJSONPath = JSONPath;

            // Check if file exists
            var fileInfo = new FileInfo(permanentJSONPath);
            var path = fileInfo.FullName;
            List<PermanentEmployee> permanentEmployees;
            if (!fileInfo.Exists)
            {
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }

            // Read the file 
            using (var file = File.OpenText(permanentJSONPath))
            {
                var serializer = new JsonSerializer();
                permanentEmployees = (List<PermanentEmployee>)serializer.Deserialize(file, typeof(List<PermanentEmployee>)) ?? new List<PermanentEmployee>();
            }

            // Search for employee based on id
            var employee = permanentEmployees.Find(x => x.EmployeeID.Equals(id));

            return employee;
        }

        private void LoadTestData()
        {
            string permanentJSONPath = JSONPath;
            // Check if permanent employee file exists
            var permanentFileInfo = new FileInfo(permanentJSONPath);
            if (permanentFileInfo.Exists) { permanentFileInfo.Delete(); }
            // create a new file
            permanentFileInfo.Create().Dispose();
            // load some data
            var permanentEmployees = new List<PermanentEmployee>()
            {
                new PermanentEmployee("PermanentPerson1",15000,100, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson01",16000,200, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson11",17000,300,new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson100",18000,400, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson101",19000,500, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson111",20000,600, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson1000",21000,700, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson1001",22000,800, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson1010",23000,900, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("PermanentPerson1011",24000,1001, new DateTimeOffset(2019,1,1,0,0,0,TimeSpan.Zero))
            };
            using (var file = File.CreateText(permanentJSONPath))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, permanentEmployees);
            }
        }
    }
}
