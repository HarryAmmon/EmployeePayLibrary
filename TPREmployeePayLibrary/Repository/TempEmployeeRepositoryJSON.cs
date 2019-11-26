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

        private List<TempEmployee> _employees;
        
        public TempEmployeeRepositoryJSON()
        {
            _employees = LoadFromFile();
#if DEBUG
            PopulateFile();
# endif
        }

        private void PopulateFile()
        {

            var fileInfo = new FileInfo(JSONPath); // Delete the file
            fileInfo.Delete();


            var data = SeedData.GetTempEmployees();
            foreach (var employee in data)
            {
                CreateTempEmployee(employee);
            }
            SaveChanges();
        }

        public TempEmployee CreateTempEmployee(TempEmployee employee)
        {
            _log.Debug($"Adding Temp Employee. EmployeeID: {employee.EmployeeID}.");

            _employees.Add(employee);

            return employee;
        }

        public bool DeleteTempEmployee(Guid id)
        {
            _log.Info($"Deleting Temp Employee. ID: {id}");

            if (!_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Warn($"Permanent Employee does not exist. ID: {id}");
                return false;
            }

            var toDelete = _employees.Find(x => x.EmployeeID.Equals(id));

            if (!_employees.Remove(toDelete))
            {
                _log.Warn($"Could not delete permanent employee. ID: {id}");
                return false;
            }

            _log.Info($"Delete succesful");
            return true;
        }

        public List<TempEmployee> ReadAllTempEmployees()
        {
            return _employees;
        }

        public TempEmployee ReadTempEmployee(Guid id)
        {
            _log.Info($"Searching for Temp Employee. ID: {id}");

            if (!_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Debug($"Temp Employee does not exist. ID: {id}");
                return null;
            }

            return _employees.Find(x => x.EmployeeID.Equals(id));
        }

        private List<TempEmployee> LoadFromFile()
        {
            var fileInfo = new FileInfo(JSONPath);
            if (fileInfo.Exists)
            {
                //_log.Info(fileInfo.FullName + " does not exist, creating file.");
                fileInfo.Delete();    
            }

            fileInfo.Directory.Create();
            fileInfo.Create().Dispose();

            using (var file = File.OpenText(JSONPath))
            {
                _log.Info(fileInfo.FullName + " has been opened.");
                var serializer = new JsonSerializer();
                var _tempEmployees = (List<TempEmployee>)serializer.Deserialize(file, typeof(List<TempEmployee>)) ?? new List<TempEmployee>();
                _log.Info(fileInfo.FullName + " has been closed.");
                return _tempEmployees;
            }
        }

        public bool UpdateTempEmployee(TempEmployee employee)
        {
            // If an employee doesn't exist then you can't update it
            _log.Info($"Updateing employee: {employee.EmployeeID}");

            var employeeToUpdate = ReadTempEmployee(employee.EmployeeID);

            if (employeeToUpdate == null)
            {
                _log.Warn("Employee does not exist.");
                return false;
            }

            // Check that the new employee has the same ID and StartDate as the one to update
            if (employeeToUpdate.EmployeeID != employee.EmployeeID)
            {
                _log.Warn("EmployeeId's do not match");
                return false;
            }
            if (employeeToUpdate.StartDate != employee.StartDate)
            {
                _log.Warn("EmployeeId's do not match");
                return false;
            }

            DeleteTempEmployee(employeeToUpdate.EmployeeID);
            CreateTempEmployee(employee);

            return SaveChanges();
        }

        public bool SaveChanges()
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
                serializer.Serialize(file, _employees);
                return true;
            }
        }
    }
}
