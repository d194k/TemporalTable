using System;
using System.Collections.Generic;

#nullable disable

namespace EMP.Entities.Models
{
    public partial class Employee
    {
        public int Employeeid { get; set; }
        public string Employeename { get; set; }
        public int Employeesalary { get; set; }
        //public DateTime Existencestartutc { get; set; }
        //public DateTime Existenceendutc { get; set; }
    }
}
