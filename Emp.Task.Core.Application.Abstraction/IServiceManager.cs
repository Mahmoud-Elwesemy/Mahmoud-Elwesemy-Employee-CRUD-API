using Employee.Core.Application.Abstraction.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Application.Abstraction;
public interface IServiceManager
{
    // Define all the services that the service manager will provide
    public IEmployeeService EmployeeService { get; }
}
