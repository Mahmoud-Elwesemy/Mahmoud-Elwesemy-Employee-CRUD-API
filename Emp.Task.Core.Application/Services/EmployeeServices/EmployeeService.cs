using AutoMapper;
using Employee.Core.Application.Abstraction.Employee;
using Employee.Core.Application.Abstraction.Employee.Model;
using Employee.Core.Domin.Entities;
using Employee.Core.Domin.Pramter_Helper;
using Employee.Core.Domin.Specifications;
using Employee.Core.Domin.UnitOfWork.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Application.Services.EmployeeServices;
internal class EmployeeService:IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _Mapper;

    public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _Mapper = mapper;
    }
    //---------------------------------------------------------------------------
    // Get Employee By Search
    public async Task<IEnumerable<EmployeeDTOWithId>> SearchAsync(string searchTerm)
    {
        var spec = new EmployeeSearchSpecification(searchTerm);
        var employees = await _unitOfWork.GetRepository<Emp,int>().GetWithSpecAsync(spec);
        return _Mapper.Map<IEnumerable<EmployeeDTOWithId>>(employees);
    }

    // Get All Employees
    public async Task<IEnumerable<EmployeeDTOWithId>> GetAllAsync(Pramter pramter)
    {
        return _Mapper.Map < IEnumerable<EmployeeDTOWithId>>
            (await _unitOfWork.GetRepository<Emp,int>().GetAllAsync(pramter));
    }
    // Get Employee By ID
    public async Task<EmployeeDTOWithId> GetByIdAsync(int id)
    {
       return _Mapper.Map<EmployeeDTOWithId>
            (await _unitOfWork.GetRepository<Emp,int>().GetByIdAsync(id));
    }
    public async Task CreateAsync(EmployeeDTO employee)
    {
        // Check if the employee is null
        if(employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        // Check if the employee already exists
        var existingEmployee = await _unitOfWork.GetRepository<Emp,int>().GetByIdAsync(employee.Id);
        if(existingEmployee != null)
        {
            throw new Exception("Employee already exists");
        }

        await _unitOfWork.GetRepository<Emp,int>().AddAsync(_Mapper.Map<Emp>(employee));
        await _unitOfWork.CompleteAsync();
    }
    // Update Employee

    public async Task UpdateAsync(EmployeeDTOWithId employee)
    {
        var EmployeeRepo = _unitOfWork.GetRepository<Emp,int>();
        var existingEmployee =await EmployeeRepo.GetByIdAsync(employee.Id);
        if(existingEmployee == null)
            throw new Exception($"Employee with ID {employee.Id} not found.");
        _Mapper.Map(employee,existingEmployee);
        EmployeeRepo.UpdateAsync(existingEmployee);
        await _unitOfWork.CompleteAsync();
    }
    // Delete Employee
    public  async Task DeleteAsync(int id)
    {
        var EmployeeRepo = _unitOfWork.GetRepository<Emp,int>();
        var existingEmployee = await EmployeeRepo.GetByIdAsync(id);
        if(existingEmployee == null)
            throw new Exception($"Employee with ID {id} not found.");
        await EmployeeRepo.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
    }

}
