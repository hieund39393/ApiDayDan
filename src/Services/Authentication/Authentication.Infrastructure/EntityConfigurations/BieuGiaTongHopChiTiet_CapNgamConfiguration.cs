using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class BieuGiaTongHopChiTiet_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<BieuGiaTongHopChiTiet_CapNgam>
    {
        public void Configure(EntityTypeBuilder<BieuGiaTongHopChiTiet_CapNgam> builder)
        {
            builder.ToTable("BieuGiaTongHopChiTiet_CapNgam");
            builder.HasKey(x => new { x.Id });
            ConfigureBase(builder);
        }
    }
}