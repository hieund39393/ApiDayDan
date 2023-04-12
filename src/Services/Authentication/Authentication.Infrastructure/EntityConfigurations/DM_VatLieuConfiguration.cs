using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_VatLieuConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_VatLieu>
    {
        public void Configure(EntityTypeBuilder<DM_VatLieu> builder)
        {
            builder.ToTable("DM_VatLieu"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenVatLieu).HasMaxLength(200); // Cấu hình độ dài tên vật liệu 
            builder.Property(x => x.MaVatLieu).HasMaxLength(50); // Cấu hình độ dài mã  vật liệu 
            builder.Property(x => x.DonViTinh).HasMaxLength(50); // Cấu hình độ dài đơn vị tính
            ConfigureBase(builder);
        }
    }
}