using EMP.Entities.Repositories.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMP.Entities.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmployeeDbContext _context;
        private IEmployeeRepository _employeeRepository = null;

        public UnitOfWork(EmployeeDbContext context)
        {
            _context = context;
        }

        public virtual IEmployeeRepository employeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_context);
                }
                return _employeeRepository;
            }
        }

        public EmployeeDbContext employeeDbContext
        {
            get
            {
                return _context;
            }
        }

        public void Commit()
        {
            try
            {
                _context.Database.BeginTransaction();
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
