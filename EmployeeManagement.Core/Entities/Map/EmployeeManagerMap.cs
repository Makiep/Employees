using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Core.Entities.Map
{
    public class EmployeeManagerMap
    {
        public EmployeeManagerMap(EntityTypeBuilder<EmployeeManager> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.ManagerId).IsRequired().HasMaxLength(50);
            entityBuilder.Property(t => t.EmployeeId).IsRequired().HasMaxLength(50);
        }
    }
}
