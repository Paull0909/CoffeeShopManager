using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    class ExportDetailsConfiguration : IEntityTypeConfiguration<ExportDetails>
    {
        public void Configure(EntityTypeBuilder<ExportDetails> builder)
        {
            builder.ToTable("ExportDetails");
            builder.HasKey(x => x.ExportDetailID);
            builder.Property(x => x.ExportDetailID).UseIdentityColumn();
            builder.HasOne(x => x.Export).WithMany(x => x.ExportDetails).HasForeignKey(x => x.ExportID);
            builder.HasOne(x => x.Lot).WithMany(x => x.ExportDetails).HasForeignKey(x => x.LotID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}