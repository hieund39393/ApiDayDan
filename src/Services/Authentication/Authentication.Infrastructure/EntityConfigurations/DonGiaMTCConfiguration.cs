using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DonGiaMTCConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaMTC>
    {
        public void Configure(EntityTypeBuilder<DonGiaMTC> builder)
        {
            builder.ToTable("DonGiaMTC"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.VanBan).HasMaxLength(50); // Cấu hình độ dài văn bản
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_MTC).WithMany(x => x.DonGiaMTC).HasForeignKey(x => x.IdMTC);

            ConfigureBase(builder);
        }
    }
}