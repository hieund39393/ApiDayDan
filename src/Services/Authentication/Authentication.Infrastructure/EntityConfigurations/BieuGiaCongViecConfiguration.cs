using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class BieuGiaCongViecConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<BieuGiaCongViec>
    {
        public void Configure(EntityTypeBuilder<BieuGiaCongViec> builder)
        {
            builder.ToTable("BieuGiaCongViec"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.HasOne(x => x.DM_Vung).WithMany(x => x.BieuGiaCongViec).HasForeignKey(x => x.VungID); // cấu hình 1-N loại vùng
            builder.HasOne(x => x.DM_KhuVuc).WithMany(x => x.BieuGiaCongViec).HasForeignKey(x => x.KhuVucID); // cấu hình 1-N loại khu vực
            builder.HasOne(x => x.DM_BieuGia).WithMany(x => x.BieuGiaCongViec).HasForeignKey(x => x.BieuGiaID); // cấu hình 1-N loại danh mục biểu giá
            builder.HasOne(x => x.DM_CongViec).WithMany(x => x.BieuGiaCongViec).HasForeignKey(x => x.CongViecID); // cấu hình 1-N công việc
            ConfigureBase(builder);
        }
    }
}