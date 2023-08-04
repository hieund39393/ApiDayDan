using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class BieuGiaTongHop_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<BieuGiaTongHop_CapNgam>
    {
        public void Configure(EntityTypeBuilder<BieuGiaTongHop_CapNgam> builder)
        {
            builder.ToTable("BieuGiaTongHop_CapNgam"); 
            builder.HasKey(x => new { x.Id }); 
            builder.HasOne(x => x.DM_BieuGia_CapNgam).WithMany(x => x.BieuGiaTongHop_CapNgam).HasForeignKey(x => x.IdBieuGia);
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,4)");
            ConfigureBase(builder);
        }
    }
}