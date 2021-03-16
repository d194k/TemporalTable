using System;
using System.Collections.Generic;
using System.Text;

namespace EMP.DomainModels
{
    public class EmployeeDto
    {
        public bool Success { get; set; }
        public EmployeeDomainModel Employee { get; set; }
    }
}
