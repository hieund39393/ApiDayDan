using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Authentication.Infrastructure.EntityConfigurations
{
    internal class ChietTinhChiTiet_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<ChietTinhChiTiet_CapNgam>
    {
        public void Configure(EntityTypeBuilder<ChietTinhChiTiet_CapNgam> builder)
        {
            builder.ToTable("ChietTinhChiTiet_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.DinhMuc).HasColumnType("numeric(18,4)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DonGiaChietTinh_CapNgam).WithMany(x => x.ChietTinhChiTiet_CapNgams).HasForeignKey(a => a.IdDonGiaChietTinh);
            ConfigureBase(builder);
        }
    }
}