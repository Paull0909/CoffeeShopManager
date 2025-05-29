using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class MaterialsConfiguration : IEntityTypeConfiguration<Materials>
    {
        public void Configure(EntityTypeBuilder<Materials> builder)
        {
            builder.ToTable("Materials");
            builder.HasKey(x => x.MaterialID);
            builder.Property(x => x.MaterialID).UseIdentityColumn();
            builder.HasOne(x => x.Categories_Material).WithMany(x => x.Materials).HasForeignKey(x => x.CategoryID);
            builder.HasOne(x => x.User).WithMany(x => x.Materials).HasForeignKey(x => x.UserID);
            builder.HasOne(x => x.Suppliers).WithMany(x => x.Materials).HasForeignKey(x => x.SupplierID);
        }
    }
}
