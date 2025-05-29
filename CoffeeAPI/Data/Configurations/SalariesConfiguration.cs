using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class SalariesConfiguration : IEntityTypeConfiguration<Salaries>
    {
        public void Configure(EntityTypeBuilder<Salaries> builder)
        {
            builder.ToTable("Salaries");
            builder.HasKey(x => x.SalaryID);
            builder.Property(x => x.SalaryID).UseIdentityColumn();
            builder.Property(x => x.Bonus).HasColumnType("decimal(18,0)");
            builder.HasOne(x => x.Employees).WithMany(x => x.Salaries).HasForeignKey(x => x.EmployeeID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.Salaries).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
