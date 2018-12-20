using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Entities.Map;


namespace EmployeeManagement.Infrastructure
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new EmployeesMap(modelBuilder.Entity<Employees>().Ignore(x => x.ManagerId));
            new EmployeesRolesMap(modelBuilder.Entity<EmployeesRoles>());
            new EmployeeManagerMap(modelBuilder.Entity<EmployeeManager>());
        }
    }
}
