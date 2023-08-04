using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Authentication.Infrastructure.EntityConfigurations
{
    internal class DonGiaChietTinh_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaChietTinh_CapNgam>
    {
        public void Configure(EntityTypeBuilder<DonGiaChietTinh_CapNgam> builder)
        {
            builder.ToTable("DonGiaChietTinh_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.DonGiaVatLieu).HasColumnType("numeric(18,4)"); // Cấu hình độ dài đơn giá
            builder.Property(x => x.DonGiaNhanCong).HasColumnType("numeric(18,4)"); // Cấu hình độ dài đơn giá
            builder.Property(x => x.DonGiaMTC).HasColumnType("numeric(18,4)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_CongViec_CapNgam).WithMany(x => x.DonGiaChietTinh_CapNgams).HasForeignKey(a => a.IdCongViec);

            ConfigureBase(builder);
        }
    }
}