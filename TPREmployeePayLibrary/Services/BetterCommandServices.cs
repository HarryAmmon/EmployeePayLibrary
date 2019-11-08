using log4net;
using System;
using System.Collections.Generic;
using TPREmployeePayLibrary.Entites;
using TPREmployeePayLibrary.Repository;

namespace TPREmployeePayLibrary.Services
{
    public class BetterCommandServices : ICommandServices
    {
        private readonly ITempEmployeeRepo _TempEmployeeRepo;
        private readonly IPermanentRepo _PermanentEmployeeRepo;
        private readonly IPermanentEmployeeServices _permanentEmployeeServices;
        private readonly ITempEmployeeServices _tempEmployeeServices;
        private readonly IEmployeeServices _employeeServices;
        private readonly ILog _log = LogManager.GetLogger(typeof(BetterCommandServices));

        public BetterCommandServices(ITempEmployeeRepo TempRepo, IPermanentRepo PermRepo, IEmployeeServices employeeServices, IPermanentEmployeeServices permanentEmployeeServices, ITempEmployeeServices tempEmployeeServices)
        {
            _TempEmployeeRepo = TempRepo;
            _PermanentEmployeeRepo = PermRepo;
            _permanentEmployeeServices = permanentEmployeeServices;
            _tempEmployeeServices = tempEmployeeServices;
            _employeeServices = employeeServices;
        }

        public virtual bool AddPermanent(string Name, string AnnualSalary, string AnnualBonus, string StartDate)
        {
            _log.Info("Permanent Employee create service called.");
            if (decimal.TryParse(AnnualSalary, out decimal annualSalary)
                && decimal.TryParse(AnnualBonus, out decimal annualBonus)
                && DateTimeOffset.TryParse(StartDate, out DateTimeOffset startDate))
            {
                var employee = new PermanentEmployee(Name, annualSalary, annualBonus, startDate);
                _log.Info("Permanent Employee created, passing object to repository.");
                _PermanentEmployeeRepo.CreatePermanentEmployee(employee);
                return true;
            }
            _log.Error($"Failed to create employee. Name: {Name}, AnnualSalary: {AnnualSalary}, AnnualBonus{AnnualBonus}, StartDate{StartDate}.");
            return false;
        }

        public virtual bool AddTemp(string Name, string DailyRate, string StartDate)
        {
            _log.Info("Temp Employee create service called");
            if (decimal.TryParse(DailyRate, out decimal dailyRate)
                && DateTimeOffset.TryParse(StartDate, out DateTimeOffset startDate))
            {
                var employee = new TempEmployee(Name, dailyRate, startDate);
                _log.Info("Temp Employee created, passing object to repository.");
                _TempEmployeeRepo.CreateTempEmployee(employee);
                return true;
            }
            _log.Error($"Failed to create employee. Name: {Name}, DailyRate: {DailyRate}, StartDate{StartDate}.");
            return false;
        }

        public virtual bool Delete(string Name, string Type)
        {
            _log.Debug($"Deleting {Type} employee");
            if (Type.Equals("temp"))
            {
                if (_TempEmployeeRepo.CheckTempEmployeeExists(Name, out TempEmployee employee))
                {
                    _log.Debug($"Employee found, passing employee object to repository.");
                    return _TempEmployeeRepo.DeleteTempEmployee(employee);
                }
                else
                {
                    _log.Debug("Employee does not exist");
                    return false;
                }
            }
            else if (Type.Equals("permanent"))
            {
                if (_PermanentEmployeeRepo.CheckPermanentEmployeeExists(Name, out PermanentEmployee employee))
                {
                    _log.Debug($"Employee found, passing employee object to repository.");
                    return _PermanentEmployeeRepo.DeletePermanentEmployee(employee);
                }
                else
                {
                    _log.Debug("Employee does not exist");
                    return false;
                }
            }
            else
            {
                _log.Error($"Unable to delete employee of type {Type}");
                return false;
            }
        }

        public bool UpdateBonus(string Name, string field, string value)
        {
            _log.Debug("UpdateBonus has been called.");
            if (_PermanentEmployeeRepo.CheckPermanentEmployeeExists(Name, out PermanentEmployee employee))
            {
                return _PermanentEmployeeRepo.UpdatePermanentEmployee(employee, field, value);
            }
            return false;
        }

        public bool UpdateDailyRate(string Name, string field, string value)
        {
            _log.Debug("UpdateDailyRate has been called");
            if (_TempEmployeeRepo.CheckTempEmployeeExists(Name, out TempEmployee employee))
            {
                return _TempEmployeeRepo.UpdateTempEmployee(employee, field, value);
            }
            return false;
        }

        public bool UpdateEndDate(string Name, string field, string value)
        {
            _log.Debug("UpdateEndDate has been called");
            if (_PermanentEmployeeRepo.CheckPermanentEmployeeExists(Name, out PermanentEmployee employee))
            {
                return _PermanentEmployeeRepo.UpdatePermanentEmployee(employee, field, value);
            }
            else if (_TempEmployeeRepo.CheckTempEmployeeExists(Name, out TempEmployee employee1))
            {
                return _TempEmployeeRepo.UpdateTempEmployee(employee1, field, value);
            }
            return false;

        }

        public bool UpdateSalary(string Name, string field, string value)
        {
            _log.Debug("UpdateSalary has been called");
            if (_PermanentEmployeeRepo.CheckPermanentEmployeeExists(Name, out PermanentEmployee employee))
            {
                return _PermanentEmployeeRepo.UpdatePermanentEmployee(employee, field, value);
            }
            return false;
        }

        public IEnumerable<string> ViewAll()
        {
            _log.Info($"View All Command has been called");
            var result = new List<string>();

            result.AddRange(ViewTemp());
            result.AddRange(ViewPermanent());

            return result;
        }

        public IEnumerable<string> View(string Name)
        {
            _log.Info($"View commmand for {Name} has been called");
            var result = new List<string>();

            _log.Info($"There are {_PermanentEmployeeRepo.ReadPermanentEmployee(Name).Count} permanent employees.");
            foreach (var employee in _PermanentEmployeeRepo.ReadPermanentEmployee(Name))
            {

                var totalAnnualPay = _permanentEmployeeServices.CalculateAnnualPay(employee.AnnualSalary, employee.AnnualBonus);
                var totalHourlyPay = _permanentEmployeeServices.CalculateHourlyPay(employee.AnnualSalary);
                result.Add(string.Format("{0,15}|{1,15}|{2,11}|{3,15}|{4,15}", employee.Name, employee.Type, employee.StartDate.Date.ToString("d"), Math.Round(totalAnnualPay, 2), Math.Round(totalHourlyPay, 2)));
            }

            _log.Info($"There are {_TempEmployeeRepo.ReadTempEmployee(Name).Count} temp employees.");
            foreach (var employee in _TempEmployeeRepo.ReadTempEmployee(Name))
            {
                var weeksWorked = _employeeServices.CalcWeeksWorked(employee.StartDate, employee.EndDate ?? DateTimeOffset.UtcNow);
                var totalAnnualPay = _tempEmployeeServices.CalculateAnnualPay(employee.DailyRate, weeksWorked);
                var totalHourlyPay = _tempEmployeeServices.CalculateHourlyPay(employee.DailyRate);
                result.Add(string.Format("{0,15}|{1,15}|{2,11}|{3,15}|{4,15}", employee.Name, employee.Type, employee.StartDate.Date.ToString("d"), Math.Round(totalAnnualPay, 2), Math.Round(totalHourlyPay, 2)));
            }
            _log.Info($"{result.Count} employees to display");
            return result;
        }

        public IEnumerable<string> ViewPermanent()
        {
            _log.Info("View Permanent employee command has been called");
            var result = new List<string>();

            foreach (var employee in _PermanentEmployeeRepo.ReadAllPermanentEmployees())
            {
                var weeksWorked = _employeeServices.CalcWeeksWorked(employee.StartDate, employee.EndDate ?? DateTimeOffset.UtcNow);
                var totalAnnualPay = _permanentEmployeeServices.CalculateAnnualPay(employee.AnnualSalary, employee.AnnualBonus);
                var totalHourlyPay = _permanentEmployeeServices.CalculateHourlyPay(employee.AnnualSalary);
                result.Add(string.Format("{0,15}|{1,15}|{2,11}|{3,15}|{4,15}", employee.Name, employee.Type, Math.Round(weeksWorked), Math.Round(totalAnnualPay, 2), Math.Round(totalHourlyPay, 2)));
            }
            _log.Info($"{result.Count} employees to display");
            return result;
        }

        public IEnumerable<string> ViewTemp()
        {
            _log.Info("View Tep employee command has been called");
            var result = new List<string>();
            foreach (var employee in _TempEmployeeRepo.ReadAllTempEmployees())
            {
                var weeksWorked = _employeeServices.CalcWeeksWorked(employee.StartDate, employee.EndDate ?? DateTimeOffset.UtcNow);
                var totalAnnualPay = _tempEmployeeServices.CalculateAnnualPay(employee.DailyRate, weeksWorked);
                var totalHourlyPay = _tempEmployeeServices.CalculateHourlyPay(employee.DailyRate);
                result.Add(string.Format("{0,15}|{1,15}|{2,11}|{3,15}|{4,15}", employee.Name, employee.Type, Math.Round(weeksWorked), Math.Round(totalAnnualPay, 2), Math.Round(totalHourlyPay, 2)));
            }
            _log.Info($"{result.Count} employees to display");
            return result;
        }
    }
}
