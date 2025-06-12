using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
     class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.HasOne(x => x.Employees).WithMany(x => x.Users).HasForeignKey(x => x.EmployeeID).OnDelete(DeleteBehavior.Restrict); ;
        }
    }
}
