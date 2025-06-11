using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class OrderToppingDetailsConfiguration : IEntityTypeConfiguration<OrderToppingDetails>
    {
        public void Configure(EntityTypeBuilder<OrderToppingDetails> builder)
        {
            builder.ToTable("OrderToppingDetails");
            builder.HasKey(x => x.OrderToppingDetailID);
            builder.Property(x => x.OrderToppingDetailID).UseIdentityColumn();
            builder.HasOne(x => x.Toppings)
                .WithMany(x => x.OrderToppingDetails)
                .HasForeignKey(x => x.ToppingID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.OrderDetails)
                .WithMany(x => x.OrderToppingDetails)
                .HasForeignKey(x => x.OrderDetailID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
