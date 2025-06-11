using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.OrderID);
            builder.Property(x => x.OrderID).UseIdentityColumn();
            builder.HasOne(x => x.Employees).WithMany(x => x.Orders).HasForeignKey(x => x.EmployeeID);
            builder.HasOne(x => x.Tables).WithMany(x => x.Orders).HasForeignKey(x => x.TableNumberID);
            builder.HasOne(x => x.Payments)
               .WithOne(x => x.Orders)
               .HasForeignKey<Payments>(x => x.OrderID)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
