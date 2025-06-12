using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.EmployeeID);
            builder.Property(x => x.EmployeeID).UseIdentityColumn();
            builder.HasOne(x => x.Positions).WithMany(x => x.Employees).HasForeignKey(x => x.PositionID);
            
        }
    }
}
