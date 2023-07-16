using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_MTCConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_MTC>
    {
        public void Configure(EntityTypeBuilder<DM_MTC> builder)
        {
            builder.ToTable("DM_MTC");
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.TenMayThiCong).HasMaxLength(200);
            builder.Property(x => x.MaMTC).HasMaxLength(50);
            ConfigureBase(builder);
        }
    }
}