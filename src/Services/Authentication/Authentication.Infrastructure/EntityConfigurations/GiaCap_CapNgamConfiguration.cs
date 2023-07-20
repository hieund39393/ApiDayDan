using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class GiaCap_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<GiaCap_CapNgam>
    {
        public void Configure(EntityTypeBuilder<GiaCap_CapNgam> builder)
        {
            builder.ToTable("DonGiaCap_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.VanBan).HasMaxLength(50); // Cấu hình độ dài văn bản
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_LoaiCap_CapNgam).WithMany(x => x.GiaCap_CapNgam).HasForeignKey(x => x.IdLoaiCap);

            ConfigureBase(builder);
        }
    }
}