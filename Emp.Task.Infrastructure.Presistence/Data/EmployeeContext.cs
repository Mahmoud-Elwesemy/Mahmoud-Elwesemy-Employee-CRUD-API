using Employee.Core.Domin.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Presistence.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        // Ensure 'Employee' refers to the correct class and not a namespace
        public virtual DbSet<Emp> Employees { get; set; }
    }
}
