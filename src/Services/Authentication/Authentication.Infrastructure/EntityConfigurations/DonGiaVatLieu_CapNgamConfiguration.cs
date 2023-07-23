using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DonGiaVatLieu_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaVatLieu_CapNgam>
    {
        public void Configure(EntityTypeBuilder<DonGiaVatLieu_CapNgam> builder)
        {
            builder.ToTable("DonGiaVatLieu_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.VanBan).HasMaxLength(50); // Cấu hình độ dài văn bản
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,2)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_VatLieu_CapNgam).WithMany(x => x.DonGiaVatLieu_CapNgam).HasForeignKey(x => x.IdVatLieu);

            ConfigureBase(builder);
        }
    }
}