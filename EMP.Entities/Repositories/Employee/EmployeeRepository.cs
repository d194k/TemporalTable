using System;
using System.Collections.Generic;
using System.Text;

namespace EMP.Entities.Repositories.Employee
{
    public class EmployeeRepository : Repository<Models.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext context)
            : base(context)
        {

        }
    }
}
