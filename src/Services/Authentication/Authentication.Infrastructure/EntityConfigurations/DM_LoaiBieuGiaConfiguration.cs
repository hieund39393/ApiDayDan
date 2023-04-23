using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_LoaiBieuGiaConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_LoaiBieuGia>
    {
        public void Configure(EntityTypeBuilder<DM_LoaiBieuGia> builder)
        {
            builder.ToTable("DM_LoaiBieuGia"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenLoaiBieuGia).HasMaxLength(500); // Cấu hình độ dài tên biểu giá
            builder.Property(x => x.MaLoaiBieuGia).HasMaxLength(100); // Cấu hình độ dài mã biểu giá
            builder.HasOne(x => x.DM_KhuVuc).WithMany(x => x.DM_LoaiBieuGia).HasForeignKey(x => x.IdKhuVuc); // cấu hình 1-N loại khu vực

            ConfigureBase(builder);
        }
    }
}