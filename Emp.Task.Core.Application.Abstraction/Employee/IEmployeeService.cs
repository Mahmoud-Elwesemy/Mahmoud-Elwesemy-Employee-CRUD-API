using Employee.Core.Application.Abstraction.Employee.Model;
using Employee.Core.Domin.Pramter_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Application.Abstraction.Employee;
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTOWithId>> SearchAsync(string searchTerm);
    Task<IEnumerable<EmployeeDTOWithId>> GetAllAsync(Pramter pramter);
    Task<EmployeeDTOWithId> GetByIdAsync(int id);
    Task CreateAsync(EmployeeDTO employee);
    Task UpdateAsync(EmployeeDTOWithId employee);
    Task DeleteAsync(int id);
}
