using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary.Repository
{
    public class PermanentEmployeeRepositoryJSON : IPermanentRepo
    {
        private readonly string JSONPath = @"..\employeeData\permanentEmployee.JSON";
        private readonly ILog _log = LogManager.GetLogger(typeof(PermanentEmployeeRepositoryJSON));

        public PermanentEmployeeRepositoryJSON()
        {
            populateFile();
        }

        private void populateFile()
        {
            var data = SeedData.GetPermanentEmployees();
            WritePermanentEmployeesToFile(data);
        }

        public PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee)
        {
            var currentPermanentEmployees = ReadPermanentEmployeesFromFile();
            _log.Debug($"Adding Permanent Employee. ENmployeeID: {employee.EmployeeID}.");
            currentPermanentEmployees.Add(employee);
            WritePermanentEmployeesToFile(currentPermanentEmployees);

            return employee;
        }

        public bool DeletePermanentEmployee(PermanentEmployee employee)
        {
            var currentPermanentEmployees = ReadPermanentEmployeesFromFile();
            _log.Debug($"Removing Permanent Employee. EmployeeID: {employee.EmployeeID}.");
            if (currentPermanentEmployees.Remove(currentPermanentEmployees.Find(x => x.EmployeeID.Equals(employee.EmployeeID))))
            {
                _log.Debug($"Permanent Employee {employee.EmployeeID} was successfully deleted.");
                WritePermanentEmployeesToFile(currentPermanentEmployees);
                return true;
            }
            else
            {
                _log.Error($"Permanent Employee {employee.EmployeeID} could not be deleted.");
                return false;
            }
        }

        public List<PermanentEmployee> ReadAllPermanentEmployees()
        {
            _log.Debug($"Reading all Permanent Employees.");
            var currentPermanentEmployees = ReadPermanentEmployeesFromFile();
            _log.Debug($"{currentPermanentEmployees.Count} employees found.");
            return currentPermanentEmployees;
        }

        public List<PermanentEmployee> ReadPermanentEmployee(string Name)
        {
            _log.Debug($"Reading all Permanent Employees with name \"{Name}\".");
            var currentPermanentEmployees = ReadPermanentEmployeesFromFile();
            var result = currentPermanentEmployees.FindAll(x => x.Name.Equals(Name));
            _log.Debug($"{result.Count} employees found.");
            return result;
        }

        public bool UpdatePermanentEmployee(PermanentEmployee employee, string field, string value)
        {
            var currentPermanentEmployees = ReadPermanentEmployeesFromFile();
            var employeeToUpdate = currentPermanentEmployees.Find(x => x.EmployeeID.Equals(employee.EmployeeID));

            switch (field)
            {
                case "name":
                    employeeToUpdate.Name = value;
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                case "salary":
                    employeeToUpdate.AnnualSalary = decimal.Parse(value);
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                case "bonus":
                    employeeToUpdate.AnnualBonus = decimal.Parse(value);
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                case "endDate":
                    employeeToUpdate.EndDate = DateTimeOffset.Parse(value);
                    _log.Debug($"Employee: {employee.EmployeeID} has had field {field} updated to {value}.");
                    break;
                default:
                    _log.Error($"{field} is not a valid type of field.");
                    return false;
            }
            WritePermanentEmployeesToFile(currentPermanentEmployees);
            return true;
        }

        private void WritePermanentEmployeesToFile(IEnumerable<PermanentEmployee> permanentEmployees)
        {
            var fileInfo = new FileInfo(JSONPath);
            if (!fileInfo.Exists)
            {
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose(); File.Create(JSONPath).Dispose();
            }
            using (var file = File.CreateText(JSONPath))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };
                serializer.Serialize(file, permanentEmployees);
            }
        }

        private List<PermanentEmployee> ReadPermanentEmployeesFromFile()
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
                var _permanentEmployees = (List<PermanentEmployee>)serializer.Deserialize(file, typeof(List<PermanentEmployee>)) ?? new List<PermanentEmployee>();
                _log.Info(fileInfo.FullName + " has been closed.");
                return _permanentEmployees;
            }
        }

        public bool CheckPermanentEmployeeExists(string Name, out PermanentEmployee employee)
        {
            var employees = ReadPermanentEmployeesFromFile();
            if (employees.Exists(x => x.Name.Equals(Name)))
            {
                employee = employees.Find(x => x.Name.Equals(Name));
                _log.Debug($"Employee \"{Name}\" exists.");
                return true;
            }
            else
            {
                _log.Warn($"Could not find permanent employee \"{Name}\".");
                employee = null;
                return false;
            }
        }
    }
}