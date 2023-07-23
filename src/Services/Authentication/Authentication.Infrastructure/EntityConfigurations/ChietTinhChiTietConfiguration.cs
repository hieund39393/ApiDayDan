using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Authentication.Infrastructure.EntityConfigurations
{
    internal class ChietTinhChiTietConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<ChietTinhChiTiet>
    {
        public void Configure(EntityTypeBuilder<ChietTinhChiTiet> builder)
        {
            builder.ToTable("ChietTinhChiTiet"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.DinhMuc).HasColumnType("numeric(18,2)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DonGiaChietTinh).WithMany(x => x.ChietTinhChiTiets).HasForeignKey(a => a.IdDonGiaChietTinh);
            ConfigureBase(builder);
        }
    }
}