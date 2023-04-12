using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.EntityConfigurations
{
    internal class DonGiaChietTinhConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaChietTinh>
    {
        public void Configure(EntityTypeBuilder<DonGiaChietTinh> builder)
        {
            builder.ToTable("DonGiaChietTinh"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_VatLieu).WithMany(x => x.DonGiaChietTinh).HasForeignKey(x => x.IdVatLieu);
            ConfigureBase(builder);
        }
    }
}