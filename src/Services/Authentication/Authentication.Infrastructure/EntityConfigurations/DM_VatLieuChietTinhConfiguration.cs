using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuChietTinhAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_VatLiecChietTinhConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_VatLieuChietTinh>
    {
        public void Configure(EntityTypeBuilder<DM_VatLieuChietTinh> builder)
        {
            builder.ToTable("DM_VatLieuChietTinh"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenVatLieuChietTinh).HasMaxLength(200); // Cấu hình độ dài tên vật liệu chiết tinh
            builder.Property(x => x.MaVatLieuChietTinh).HasMaxLength(50); // Cấu hình độ dài mã  vật liệu chiết tinh
            builder.Property(x => x.DonViTinh).HasMaxLength(50); // Cấu hình độ dài đơn vị tính
            ConfigureBase(builder);
        }
    }
}