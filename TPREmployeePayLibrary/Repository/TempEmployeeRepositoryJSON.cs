using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public class TempEmployeeRepositoryJSON : ITempEmployeeRepo
    {
        private readonly string JSONPath = @"..\employeeData\tempEmployee.JSON";
        private readonly ILog _log = LogManager.GetLogger(typeof(TempEmployeeRepositoryJSON));

        public TempEmployeeRepositoryJSON()
        {
            PopulateFile(); 
        }

        private void PopulateFile()
        {
            var data = SeedData.GetTempEmployees();
            WriteTempEmployeesToFile(data);
        }

        public bool CheckTempEmployeeExists(string Name, out TempEmployee employee)
        {
            var employees = ReadTempEmployeesFromFile();
            if (employees.Exists(x => x.Name.Equals(Name)))
            {
                _log.Debug($"Employee \"{Name}\" exists.");
                employee = employees.Find(x => x.Name.Equals(Name));
                return true;
            }
            else
            {
                _log.Warn($"Could not find temp employee \"{Name}\".");
                employee = null;
                return false;
            }
        }

        public TempEmployee CreateTempEmployee(TempEmployee employee)
        {
            var currentTempEmployees = ReadTempEmployeesFromFile();
            _log.Debug($"Adding Temp Employee. EmployeeID: {employee.EmployeeID}.");
            currentTempEmployees.Add(employee);
            WriteTempEmployeesToFile(currentTempEmployees);

            return employee;
        }

        public bool DeleteTempEmployee(TempEmployee employee)
        {
            var currentTempEmployees = ReadTempEmployeesFromFile();
            _log.Debug($"Removing Temp employee. EmployeeID: {employee.EmployeeID}.");
            if (currentTempEmployees.Remove(currentTempEmployees.Find(x => x.EmployeeID.Equals(employee.EmployeeID))))
            {
                _log.Info($"Temp Employee {employee.EmployeeID} was successfully deleted.");
                WriteTempEmployeesToFile(currentTempEmployees);
                return true;
            }
            else
            {
                _log.Error($"Temp Employee {employee.EmployeeID} could not be removed.");
                return false;
            }
        }

        public List<TempEmployee> ReadAllTempEmployees()
        {
            _log.Debug($"Reading all Temp Employees.");
            var currentTempEmployees = ReadTempEmployeesFromFile();
            _log.Debug($"{currentTempEmployees.Count} employees found.");
            return currentTempEmployees;
        }

        public List<TempEmployee> ReadTempEmployee(string Name)
        {
            _log.Debug($"Reading all Temp Employees with name \"{Name}\".");
            var tempEmployees = ReadTempEmployeesFromFile();
            var result = tempEmployees.FindAll(x => x.Name.Equals(Name));
            _log.Debug($"{result.Count} employees found.");
            return result;
        }

        private List<TempEmployee> ReadTempEmployeesFromFile()
        {
            var fileInfo = new FileInfo(JSONPath);
            if (!fileInfo.Exists)
            {
                _log.Info(fileInfo.FullName + " does not exist, creating file.");
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }
            using (var file = File.OpenText(JSONPath))
            {
                _log.Info(fileInfo.FullName + " has been opened.");
                var serializer = new JsonSerializer();
                var _tempEmployees = (List<TempEmployee>)serializer.Deserialize(file, typeof(List<TempEmployee>)) ?? new List<TempEmployee>();
                _log.Info(fileInfo.FullName + " has been closed.");
                return _tempEmployees;
            }
        }

        public bool UpdateTempEmployee(TempEmployee employee, string field, string value)
        {
            var currentTempEmployees = ReadTempEmployeesFromFile();
            var employeeToUpdate = currentTempEmployees.Find(x => x.EmployeeID.Equals(employee.EmployeeID));

            switch (field)
            {
                case "name":
                    employeeToUpdate.Name = value;
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                case "dailyrate":
                    employeeToUpdate.DailyRate = decimal.Parse(value);
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                case "endDate":
                    employeeToUpdate.EndDate = DateTimeOffset.Parse(value);
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                default:
                    _log.Error($"{field} is not a valid type of field.");
                    throw new Exception("Field could not be found");
            }

            WriteTempEmployeesToFile(currentTempEmployees);

            return true;
        }

        private void WriteTempEmployeesToFile(IEnumerable<TempEmployee> tempEmployees)
        {
            var fileInfo = new FileInfo(JSONPath);
            if (!fileInfo.Exists)
            {
                _log.Info(fileInfo.FullName + " does not exist, creating file.");
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }
            using (var file = File.CreateText(JSONPath))
            {
                _log.Info(fileInfo.FullName + " has been opened.");
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(file, tempEmployees);
                _log.Info(fileInfo.FullName + " has been closed.");
            }
        }
    }
}
