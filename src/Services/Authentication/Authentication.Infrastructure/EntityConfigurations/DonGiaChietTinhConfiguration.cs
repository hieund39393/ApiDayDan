using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Authentication.Infrastructure.EntityConfigurations
{
    internal class DonGiaChietTinhConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaChietTinh>
    {
        public void Configure(EntityTypeBuilder<DonGiaChietTinh> builder)
        {
            builder.ToTable("DonGiaChietTinh"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.DonGiaVatLieu).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.Property(x => x.DonGiaNhanCong).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.Property(x => x.DonGiaMTC).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_CongViec).WithMany(x => x.DonGiaChietTinhs).HasForeignKey(a => a.IdCongViec);

            ConfigureBase(builder);
        }
    }
}