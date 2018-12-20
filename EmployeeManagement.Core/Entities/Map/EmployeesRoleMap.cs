using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Core.Entities.Map
{
    public class EmployeesRolesMap
    {
        public EmployeesRolesMap(EntityTypeBuilder<EmployeesRoles> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.RoleName).IsRequired().HasMaxLength(50);
        }
    }
}
