using Autofac;
using EMP.Entities;
using EMP.Entities.UOW;
using EMP.Services;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;

namespace EmployeeConsole
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            _container = GetIocContainer();

            if (args.Length > 0)
            {                
                switch (args[0])
                {
                    case "set-employee":
                        SetEmployee(args);
                        break;
                    case "get-employee":
                        GetEmployee(args);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public static void SetEmployee(string[] args)
        {
            var param = args.Skip(1).ToArray();
            var rootCommand = new RootCommand {
                new Option<string> ("--employeeId", description: "Employee Id"),
                new Option<string>("--employeeName", description: "Employee name"),
                new Option<string>("--employeeSalary", description: "Employee salary"),                
            };
            rootCommand.Description = "Set employee command";
            rootCommand.Handler = CommandHandler.Create<string, string, string>(ExecuteSetEmployee);
            rootCommand.Invoke(param);
        }

        public static void ExecuteSetEmployee(string employeeId, string employeeName, string employeeSalary)
        {
            int empId = 0;
            int empSalary = 0;

            try
            {
                var employeeService = _container.Resolve<EmployeeService>();
                if (employeeId != null && employeeName != null && employeeSalary != null && int.TryParse(employeeId, out empId) && int.TryParse(employeeSalary, out empSalary) && empId > 0 && empSalary > 0)
                {
                    var success = employeeService.SetEmployee(empId,employeeName, empSalary);
                    if (success)
                    {
                        Console.WriteLine("set-employee success");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void GetEmployee(string[] args)
        {
            var employeeService = _container.Resolve<EmployeeService>();
            var param = args.Skip(1).ToArray();
            var rootCommand = new RootCommand {
                new Option<string> ("--employeeId", description: "Employee Id"),
                new Option<string>("--simulatedTimeUtc",() => DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"), description: "Timestamp"),
            };
            rootCommand.Description = "Get employee command";
            rootCommand.Handler = CommandHandler.Create<string, string>(ExecuteGetEmployee);
            rootCommand.Invoke(param);
        }

        public static void ExecuteGetEmployee(string employeeId, string simulatedTimeUtc)
        {
            int empId = 0;
            DateTime simTime = DateTime.Now;

            try
            {
                var employeeService = _container.Resolve<EmployeeService>();
                if (employeeId != null && simulatedTimeUtc != null && int.TryParse(employeeId, out empId) && DateTime.TryParse(simulatedTimeUtc, out simTime) && empId > 0)
                {
                    var result = employeeService.GetEmployee(empId, simTime);
                    if (result != null && result.Success)
                    {
                        Console.WriteLine($"Employee Id: {result.Employee.EmployeeId}");
                        Console.WriteLine($"Employee Name: {result.Employee.EmployeeName}");
                        Console.WriteLine($"Employee Salary: {result.Employee.EmployeeSalary}");
                    }
                    else
                    {
                        Console.WriteLine("Employee not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static IContainer GetIocContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            containerBuilder.RegisterType<EmployeeDbContext>().AsSelf();
            containerBuilder.RegisterType<EmployeeService>().AsSelf();
            return containerBuilder.Build();
        }
    }
}
