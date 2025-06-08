using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class LotDetailsConfiguration : IEntityTypeConfiguration<LotDetails>
    {
        public void Configure(EntityTypeBuilder<LotDetails> builder)
        {
            builder.ToTable("LotDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Lot).WithMany(x => x.LotDetails).HasForeignKey(x => x.LotId);
        }
    }
}
