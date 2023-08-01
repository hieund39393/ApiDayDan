using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DonGiaVatLieuConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaVatLieu>
    {
        public void Configure(EntityTypeBuilder<DonGiaVatLieu> builder)
        {
            builder.ToTable("DonGiaVatLieu"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.VanBan).HasMaxLength(50); // Cấu hình độ dài văn bản
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,4)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_VatLieu).WithMany(x => x.DonGiaVatLieu).HasForeignKey(x => x.IdVatLieu);

            ConfigureBase(builder);
        }
    }
}