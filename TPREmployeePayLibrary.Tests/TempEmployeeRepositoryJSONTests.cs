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
    public class TempEmployeeRepoJSONTest
    {
        private readonly TempEmployeeRepositoryJSON repo;
        private readonly Mock<ILog> _log;
        private readonly string JSONPath = @"..\employeeData\tempEmployee.JSON";

        public TempEmployeeRepoJSONTest()
        {
            _log = new Mock<ILog>();
            repo = new TempEmployeeRepositoryJSON();
            LoadTestData();
        }

        [Fact]
        public void Can_Add_New_Temp_Employee()
        {
            // Arrange
            var employee = new TempEmployee("Josh Smith", 19.2m, new DateTimeOffset(2012, 8, 1, 0, 0, 0, TimeSpan.Zero));
            var expectedResult = employee.EmployeeID;

            // Act
            repo.CreateTempEmployee(employee);
            var actualResult = SearchJSONForTempEmployee(employee.EmployeeID).EmployeeID;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Can_Delete_Temp_Employee()
        {
            // Arrange
            var employeeToDelete = SearchJSONForTempEmployee("TempPerson101")[0];

            // Act
            repo.DeleteTempEmployee(employeeToDelete);
            var expectedResult = SearchJSONForTempEmployee(employeeToDelete.EmployeeID);

            // Assert
            Assert.Null(expectedResult);
        }

        [Fact]
        public void Can_Read_All_Temp_Employees()
        {
            // Arrange

            // Act
            var list = repo.ReadAllTempEmployees();

            // Assert
            Assert.NotNull(list);
        }

        [Fact]
        public void Can_Read_Temp_Employee_Based_On_Name()
        {
            // Arrange
            var employee = SearchJSONForTempEmployee("TempPerson100")[0];

            // Act
            var actualResult = repo.ReadTempEmployee(employee.Name)[0];

            // Assert
            Assert.Equal(employee.EmployeeID, actualResult.EmployeeID);
        }

        [Fact]
        public void Can_Update_TempEmployee_Name()
        {
            // Arrange
            var employeeToUpdate = SearchJSONForTempEmployee("TempPerson1")[0];
            var employeeID = employeeToUpdate.EmployeeID;
            var expectedName = "Samantha";

            // Act
            repo.UpdateTempEmployee(employeeToUpdate, "name", expectedName);
            var actualName = SearchJSONForTempEmployee(employeeID).Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Can_Update_TempEmployee_DailyRate()
        {
            // Arrange
            var employeeToUpdate = SearchJSONForTempEmployee("TempPerson11")[0];
            var employeeID = employeeToUpdate.EmployeeID;
            var expectedDailyRate = "62.11";

            // Act
            repo.UpdateTempEmployee(employeeToUpdate, "dailyrate", expectedDailyRate);
            var actualDailyRate = SearchJSONForTempEmployee(employeeID).DailyRate;

            // Assert
            Assert.Equal(decimal.Parse(expectedDailyRate), actualDailyRate);
        }


        private TempEmployee SearchJSONForTempEmployee(Guid id)
        {
            string tempJSONPath = JSONPath;

            // Check if file exists
            var fileInfo = new FileInfo(tempJSONPath);
            var path = fileInfo.FullName;
            List<TempEmployee> tempEmployees;
            if (!fileInfo.Exists)
            {
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }

            // Read the file 
            using (var file = File.OpenText(tempJSONPath))
            {
                var serializer = new JsonSerializer();
                tempEmployees = (List<TempEmployee>)serializer.Deserialize(file, typeof(List<TempEmployee>)) ?? new List<TempEmployee>();
            }

            // Search for employee based on id
            var employee = tempEmployees.Find(x => x.EmployeeID.Equals(id));

            return employee;
        }

        private List<TempEmployee> SearchJSONForTempEmployee(string Name)
        {
            string tempJSONPath = JSONPath;


            // Check if file exists
            var fileInfo = new FileInfo(tempJSONPath);
            var path = fileInfo.FullName;
            List<TempEmployee> tempEmployees;
            if (!fileInfo.Exists)
            {
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }

            // Read the file 
            using (var file = File.OpenText(tempJSONPath))
            {
                var serializer = new JsonSerializer();
                tempEmployees = (List<TempEmployee>)serializer.Deserialize(file, typeof(List<TempEmployee>)) ?? new List<TempEmployee>();
            }

            // Search for employee based on id
            var employee = tempEmployees.FindAll(x => x.Name.Equals(Name));

            return employee;
        }

        private void LoadTestData()
        {
            string tempJSONPath = JSONPath;

            // Check if temp employee file exists
            var tempFileInfo = new FileInfo(tempJSONPath);
            if (tempFileInfo.Exists) { tempFileInfo.Delete(); }
            // create a new file
            tempFileInfo.Directory.Create();
            tempFileInfo.Create().Dispose();
            // load some data
            var tempEmployees = new List<TempEmployee>()
            {
                new TempEmployee("TempPerson1",21.1m, new DateTimeOffset(2019,2,2,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson01",21.2m, new DateTimeOffset(2019,2,3,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson11",21.3m, new DateTimeOffset(2019,2,4,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson100",21.4m, new DateTimeOffset(2019,2,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson101",21.5m, new DateTimeOffset(2019,2,6,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson111",21.6m, new DateTimeOffset(2019,2,7,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson1000",21.7m, new DateTimeOffset(2019,2,8,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson1001",21.8m, new DateTimeOffset(2019,2,9,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson1010",21.9m, new DateTimeOffset(2019,2,10,0,0,0,TimeSpan.Zero)),
                new TempEmployee("TempPerson1011",22m, new DateTimeOffset(2019,2,11,0,0,0,TimeSpan.Zero)),

            };

            using (var file = File.CreateText(tempJSONPath))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(file, tempEmployees);
            }
            // Check if permanent employee file exists
        }
    }
}
