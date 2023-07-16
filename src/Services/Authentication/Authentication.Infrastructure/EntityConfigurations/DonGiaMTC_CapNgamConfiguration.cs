using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DonGiaMTC_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaMTC_CapNgam>
    {
        public void Configure(EntityTypeBuilder<DonGiaMTC_CapNgam> builder)
        {
            builder.ToTable("DonGiaMTC_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.VanBan).HasMaxLength(50); // Cấu hình độ dài văn bản
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_MTC_CapNgam).WithMany(x => x.DonGiaMTC_CapNgam).HasForeignKey(x => x.IdMTC);

            ConfigureBase(builder);
        }
    }
}