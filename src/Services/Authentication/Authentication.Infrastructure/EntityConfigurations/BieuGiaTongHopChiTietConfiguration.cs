using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class BieuGiaTongHopChiTietConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<BieuGiaTongHopChiTiet>
    {
        public void Configure(EntityTypeBuilder<BieuGiaTongHopChiTiet> builder)
        {
            builder.ToTable("BieuGiaTongHopChiTiet");
            builder.HasKey(x => new { x.Id });
            ConfigureBase(builder);
        }
    }
}