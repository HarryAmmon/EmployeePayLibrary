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
            //PopulateFile(); 
            _employees = LoadFromFile();
        }

        private void PopulateFile()
        {
            _employees = SeedData.GetTempEmployees();
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

            if(!_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Debug($"Temp Employee does not exist. ID: {id}");
                return null;
            }

            return _employees.Find(x => x.EmployeeID.Equals(id));
        }

        private List<TempEmployee> LoadFromFile()
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

        public bool UpdateTempEmployee(Guid id, string field, string value)
        {
            _log.Info($"Updating field: {field} for employee {id}");

            if (_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Warn($"Temp Employee does not exist. ID: {id}");
                return false;
            }

            var toUpdate = _employees.Find(x => x.EmployeeID.Equals(id));

            try
            {
                var fieldToUpdate = typeof(TempEmployee).GetType().GetProperty(field);

                fieldToUpdate.SetValue(toUpdate, value);

                return true;
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(ex.Message);
                return false;
            }
            catch (System.Reflection.AmbiguousMatchException ex)
            {
                _log.Error(ex.Message);
                return false;
            }
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
