using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_BieuGiaConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_BieuGia>
    {
        public void Configure(EntityTypeBuilder<DM_BieuGia> builder)
        {
            builder.ToTable("DM_BieuGia"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenBieuGia).HasMaxLength(200); // Cấu hình độ dài tên biểu giá
            builder.Property(x => x.MaBieuGia).HasMaxLength(20); // Cấu hình độ dài mã biểu giá
             builder.HasOne(x => x.DM_LoaiBieuGia).WithMany(x => x.DM_BieuGia).HasForeignKey(x => x.idLoaiBieuGia); // cấu hình 1-N loại biểu giá

            ConfigureBase(builder);
        }
    }
}