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

        private List<PermanentEmployee> _employees;

        public PermanentEmployeeRepositoryJSON()
        {
            //populateFile();
            LoadFromFile();
        }

        //private void populateFile()
        //{
        //    var data = SeedData.GetPermanentEmployees();
        //    WritePermanentEmployeesToFile(data);
        //}

        public PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee)
        {
            _log.Debug($"Adding Permanent Employee. EmployeeID: {employee.EmployeeID}.");
            _employees.Add(employee);
            return employee;
        }

        public bool DeletePermanentEmployee(Guid id)
        {
            _log.Info($"Deleting Permanent Employee. ID: {id}");

            if(!_employees.Exists(x => x.EmployeeID.Equals(id)))
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

        public List<PermanentEmployee> ReadAllPermanentEmployees()
        {
            return _employees;
        }

        public PermanentEmployee ReadPermanentEmployee(Guid id)
        {
            _log.Info($"Searching for Permanent Employee. ID: {id}");

            if(!_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Debug($"Employee does not exist. ID: {id}");
                return null;
            }

            return _employees.Find(x => x.EmployeeID.Equals(id));
           
        }

        public bool UpdatePermanentEmployee(Guid id, string field, string value)
        {
            _log.Info($"Updating field: {field} for employee {id}");
            
            if (_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Warn($"Permanent Employee does not exist. ID: {id}");
                return false;
            }
            
            var toUpdate = _employees.Find(x => x.EmployeeID.Equals(id));

            try
            {

                var fieldToUpdate = typeof(PermanentEmployee).GetType().GetProperty(field);

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

        private void LoadFromFile()
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
                _employees = (List<PermanentEmployee>)serializer.Deserialize(file, typeof(List<PermanentEmployee>)) ?? new List<PermanentEmployee>();
            }

            _log.Info(fileInfo.FullName + " has been closed.");

        }

        //public bool CheckPermanentEmployeeExists(string Name, out PermanentEmployee employee)
        //{
        //    var employees = ReadPermanentEmployeesFromFile();
        //    if (employees.Exists(x => x.Name.Equals(Name)))
        //    {
        //        employee = employees.Find(x => x.Name.Equals(Name));
        //        _log.Debug($"Employee \"{Name}\" exists.");
        //        return true;
        //    }
        //    else
        //    {
        //        _log.Warn($"Could not find permanent employee \"{Name}\".");
        //        employee = null;
        //        return false;
        //    }
        //}
    }
}