using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class SurchargesConfiguration : IEntityTypeConfiguration<Surcharges>
    {
        public void Configure(EntityTypeBuilder<Surcharges> builder)
        {
            builder.ToTable("Surcharges");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).UseIdentityColumn();
        }
    }
}
