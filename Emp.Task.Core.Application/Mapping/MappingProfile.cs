using AutoMapper;
using Employee.Core.Application.Abstraction.Employee.Model;
using Employee.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Application.Mapping;
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Emp,EmployeeDTO>().ReverseMap();
        CreateMap<Emp,EmployeeDTOWithId>().ReverseMap();
    }
}
