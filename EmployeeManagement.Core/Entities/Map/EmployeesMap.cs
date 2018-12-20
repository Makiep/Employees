using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Core.Entities.Map
{
    public class EmployeesMap
    {
        public EmployeesMap(EntityTypeBuilder<Employees> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(t => t.LastName).IsRequired().HasMaxLength(50);
            entityBuilder.Property(t => t.Password).IsRequired().HasMaxLength(50);
        }
    }
}
