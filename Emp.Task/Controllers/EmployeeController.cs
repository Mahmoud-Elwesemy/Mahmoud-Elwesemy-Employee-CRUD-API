using Employee.Core.Application.Abstraction;
using Employee.Core.Application.Abstraction.Employee.Model;
using Employee.Core.Domin.Pramter_Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController:ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public EmployeeController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("GetAllEmployee")]
    public async Task<ActionResult<IEnumerable<EmployeeDTOWithId>>> GetAllEmployee([FromQuery] Pramter pramter)
    {
        var employees = await _serviceManager.EmployeeService.GetAllAsync(pramter);
        return Ok(employees);
    }
    [HttpGet("Search")]
    public async Task<ActionResult<IEnumerable<EmployeeDTOWithId>>> Search([FromQuery] string searchTerm)
    {
        var result = await _serviceManager.EmployeeService.SearchAsync(searchTerm);
        return Ok(result);
    }
    [HttpGet("GetEmployeeById/{id}")]
    public async Task<ActionResult<EmployeeDTOWithId>> GetEmployeeById(int id)
    {
        var employee = await _serviceManager.EmployeeService.GetByIdAsync(id);
        if(employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }
    [HttpPost("CreateEmployee")]
    public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] EmployeeDTO employee)
    {
        if(employee == null)
        {
            return BadRequest("Invalid Employee data");
        }
        await _serviceManager.EmployeeService.CreateAsync(employee);
        return Ok();
    }
    [HttpPut("UpdateEmployee/{id}")]
    public async Task<ActionResult<EmployeeDTOWithId>> UpdateEmployee(int id,[FromBody] EmployeeDTOWithId employee)
    {
        if(employee == null || id != employee.Id)
            return BadRequest("Invalid Employee data.");
        try
        {
            await _serviceManager.EmployeeService.UpdateAsync(employee);
            return NoContent(); // 204 No Content (successful update)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("DeleteEmployee/{id}")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        try
        {
            await _serviceManager.EmployeeService.DeleteAsync(id);
            return NoContent(); // 204 No Content (successful deletion)
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
