using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lot");
            builder.HasKey(x => x.LotID);
            builder.Property(x => x.LotID).UseIdentityColumn();
            builder.HasOne(x => x.Materials).WithMany(x => x.Lots).HasForeignKey(x => x.MaterialID);
        }
    }
}
