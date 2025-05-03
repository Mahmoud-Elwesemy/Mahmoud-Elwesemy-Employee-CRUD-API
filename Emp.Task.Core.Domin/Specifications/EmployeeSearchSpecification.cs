using Employee.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Domin.Specifications;
public class EmployeeSearchSpecification:BaseSpecification<Emp>
{
    public EmployeeSearchSpecification(string searchTerm)
        : base(e =>
            string.IsNullOrEmpty(searchTerm) ||
            e.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
            e.LastName.ToLower().Contains(searchTerm.ToLower()))
    {
    }
}
