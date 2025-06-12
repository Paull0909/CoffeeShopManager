using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class PositionsConfiguration : IEntityTypeConfiguration<Positions>
    {
        public void Configure(EntityTypeBuilder<Positions> builder)
        {
            builder.ToTable("Positions");
            builder.HasKey(x => x.PositionID);
            builder.Property(x => x.PositionID).UseIdentityColumn();
        }
    }
}
