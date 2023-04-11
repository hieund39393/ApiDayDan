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
            builder.HasOne(x => x.DM_BieuGias).WithMany(x => x.BieuGiaCongViec).HasForeignKey(x => x.IdBieuGia); // cấu hình 1-N loại danh mục biểu giá
            builder.HasOne(x => x.DM_CongViecs).WithMany(x => x.BieuGiaCongViec).HasForeignKey(x => x.IdCongViec); // cấu hình 1-N công việc
            ConfigureBase(builder);
        }
    }
}