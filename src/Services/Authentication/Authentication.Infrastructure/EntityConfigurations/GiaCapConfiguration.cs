using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class GiaCapConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<GiaCap>
    {
        public void Configure(EntityTypeBuilder<GiaCap> builder)
        {
            builder.ToTable("DonGiaCap"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.VanBan).HasMaxLength(50); // Cấu hình độ dài văn bản
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,4)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.DM_LoaiCap).WithMany(x => x.GiaCap).HasForeignKey(x => x.IdLoaiCap);

            ConfigureBase(builder);
        }
    }
}