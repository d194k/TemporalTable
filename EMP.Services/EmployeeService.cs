using EMP.Entities.UOW;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EMP.Entities;
using EMP.Entities.Models;
using System.Dynamic;
using EMP.DomainModels;

namespace EMP.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool SetEmployee(int employeeId, string employeeName, int employeeSalary)
        {
            bool success = false;
            try
            {
                var employee = _unitOfWork.employeeRepository.Get(x => x.Employeeid == employeeId).FirstOrDefault();
                if (employee != null)
                {
                    employee.Employeeid = employeeId;
                    employee.Employeename = employeeName;
                    employee.Employeesalary = employeeSalary;
                }
                else
                {
                    employee = new Employee();
                    employee.Employeeid = employeeId;
                    employee.Employeename = employeeName;
                    employee.Employeesalary = employeeSalary;
                    _unitOfWork.employeeRepository.Add(employee);
                }
                _unitOfWork.Commit();
                success = true;
            }
            catch (Exception)
            {
                throw;
            }
            return success;
        }

        public EmployeeDto GetEmployee(int employeeId, DateTime simulatedTime)
        {
            Employee employee = null;
            EmployeeDto result = new EmployeeDto() { Success = false, Employee = null};
            try
            {
                var utcTime = simulatedTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                employee = _unitOfWork.employeeDbContext.Employees
                    .FromSqlRaw($"select * from employees for system_time as of '{utcTime}'")
                    .Where(x => x.Employeeid == employeeId)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (employee != null)
                {
                    result.Success = true;
                    result.Employee = new EmployeeDomainModel {
                        EmployeeId = employee.Employeeid,
                        EmployeeName = employee.Employeename,
                        EmployeeSalary = employee.Employeesalary
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
