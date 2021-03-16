using EMP.Entities.Repositories.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMP.Entities.UOW
{
    public interface IUnitOfWork
    {
        IEmployeeRepository employeeRepository { get; }
        EmployeeDbContext employeeDbContext { get; }
        void Commit();
    }
}
