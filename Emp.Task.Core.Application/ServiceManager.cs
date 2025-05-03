using AutoMapper;
using Employee.Core.Application.Abstraction;
using Employee.Core.Application.Abstraction.Employee;
using Employee.Core.Application.Services.EmployeeServices;
using Employee.Core.Domin.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Application;
public class ServiceManager:IServiceManager
{
    // Lazy loading of services to improve performance
    // This allows the services to be created only when they are accessed for the first time.
    // This can help reduce the startup time of the application and improve overall performance.
    // It also helps to avoid unnecessary instantiation of services that may not be used.
    // This is particularly useful in scenarios where the services are expensive to create or have dependencies that may not be needed immediately.
    // By using Lazy<T>, the services are created only when they are accessed, which can help improve performance.
    // Lazy<T> is a thread-safe way to create objects only when they are needed.
    // This can help improve performance by avoiding unnecessary instantiation of services that may not be used.

    private readonly Lazy<IEmployeeService> _employeeService;
    public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper)
    {
        // Initialize the services using Lazy<T> to defer their creation until they are accessed 
        _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork,mapper));
    }
    // Properties to access the services
    public IEmployeeService EmployeeService => _employeeService.Value;
}
