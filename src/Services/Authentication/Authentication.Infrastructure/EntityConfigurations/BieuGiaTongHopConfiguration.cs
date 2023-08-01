using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class BieuGiaTongHopConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<BieuGiaTongHop>
    {
        public void Configure(EntityTypeBuilder<BieuGiaTongHop> builder)
        {
            builder.ToTable("BieuGiaTongHop"); 
            builder.HasKey(x => new { x.Id }); 
            builder.HasOne(x => x.DM_BieuGia).WithMany(x => x.BieuGiaTongHop).HasForeignKey(x => x.IdBieuGia);
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,4)");
            ConfigureBase(builder);
        }
    }
}