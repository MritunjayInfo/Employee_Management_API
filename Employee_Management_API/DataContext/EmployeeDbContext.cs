using Employee_Management_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Employee_Management_API.DataContext
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
