using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Employee.Core.Application.Abstraction.Employee.Model;
public record EmployeeDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
}
public record EmployeeDTOWithId:EmployeeDTO
{
    public int Id { get; set; }
}
