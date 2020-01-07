using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TPREmployeePayLibrary.Entities;

namespace TPREmployeePayLibrary.Repository
{
    public class PermanentEmployeeRepositoryJSON : IPermanentRepo
    {
        private readonly string JSONPath = @"..\employeeData\permanentEmployee.JSON";
        private readonly ILog _log = LogManager.GetLogger(typeof(PermanentEmployeeRepositoryJSON));

        private readonly List<PermanentEmployee> _employees;
        
        public PermanentEmployeeRepositoryJSON()
        {
            _employees = LoadFromFile();
#if DEBUG
            PopulateFile();
#endif

        }

        private void PopulateFile()
        {
            var fileInfo = new FileInfo(JSONPath);
            fileInfo.Delete();

            var data = SeedData.GetPermanentEmployees();
            foreach (var employee in data)
            {
                CreatePermanentEmployee(employee);
            }

            SaveChanges();
        }

        public PermanentEmployee CreatePermanentEmployee(PermanentEmployee employee)
        {
            _log.Debug($"Adding Permanent Employee. EmployeeID: {employee.EmployeeID}.");
            if (employee.EmployeeID == 0)
            {
                var rnd = new Random();
                employee.EmployeeID = rnd.Next(1,999999);
            }
            _employees.Add(employee);
            return employee;
        }

        public bool DeletePermanentEmployee(int id)
        {
            _log.Info($"Deleting Permanent Employee. ID: {id}");

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

        public List<PermanentEmployee> ReadAllPermanentEmployees()
        {
            return _employees;
        }

        public PermanentEmployee ReadPermanentEmployee(int id)
        {
            _log.Info($"Searching for Permanent Employee. ID: {id}");

            if (!_employees.Exists(x => x.EmployeeID.Equals(id)))
            {
                _log.Debug($"Employee does not exist. ID: {id}");
                return null;
            }

            return _employees.Find(x => x.EmployeeID.Equals(id));
        }

        public bool SaveChanges()
        {
            var fileInfo = new FileInfo(JSONPath);
            if (!fileInfo.Exists)
            {
                fileInfo.Directory.Create();
                fileInfo.Create().Dispose();
            }
            using var file = File.CreateText(JSONPath);
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
            serializer.Serialize(file, _employees);
            return true;
        }

        private List<PermanentEmployee> LoadFromFile()
        {
            var fileInfo = new FileInfo(JSONPath);
            if (fileInfo.Exists)
            {
                //_log.Info(fileInfo.FullName + " does not exist, creating file.");
                fileInfo.Delete();
            }

            fileInfo.Directory.Create();
            fileInfo.Create().Dispose();

            using var file = File.OpenText(JSONPath);
            _log.Info(fileInfo.FullName + " has been opened.");
            var serializer = new JsonSerializer();
            var _permEmployees = (List<PermanentEmployee>)serializer.Deserialize(file, typeof(List<PermanentEmployee>)) ?? new List<PermanentEmployee>();
            _log.Info(fileInfo.FullName + " has been closed.");
            return _permEmployees;
        }

        public bool UpdatePermanentEmployee(PermanentEmployee employee)
        {
            // If an employee doesn't exist then you can't update it
            _log.Info($"Updateing employee: {employee.EmployeeID}");
            
            var employeeToUpdate = ReadPermanentEmployee(employee.EmployeeID);

            if(employeeToUpdate == null)
            {
                _log.Warn("Employee could not be found. Was the Id changed?");
                return false;
            }

            // Check that the new employee has the same ID and StartDate as the one to update
            if(employeeToUpdate.EmployeeID != employee.EmployeeID)
            {
                _log.Warn("EmployeeId's do not match");
                return false;
            }
            if (employeeToUpdate.StartDate.Date != employee.StartDate.Date)
            {
                _log.Warn("EmployeeId's do not match");
                return false;
            }

            DeletePermanentEmployee(employeeToUpdate.EmployeeID);
            CreatePermanentEmployee(employee);

            return SaveChanges();
        }

        public Task<PermanentEmployee> ReadPermanentEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PermanentEmployee>> ReadAllPermanentEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PermanentEmployee> CreatePermanentEmployeeAsync(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }

        public Task DeletePermanentEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PermanentEmployee> UpdatePermanentEmployeeAsync(PermanentEmployee employee)
        {
            throw new NotImplementedException();
        }
    }
}