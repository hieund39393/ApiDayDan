using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class BieuGiaCongViec_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<BieuGiaCongViec_CapNgam>
    {
        public void Configure(EntityTypeBuilder<BieuGiaCongViec_CapNgam> builder)
        {
            builder.ToTable("BieuGiaCongViec_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.HasOne(x => x.DM_BieuGia_CapNgam).WithMany(x => x.BieuGiaCongViec_CapNgam).HasForeignKey(x => x.IdBieuGia); // cấu hình 1-N loại danh mục biểu giá
            builder.HasOne(x => x.DM_CongViec_CapNgam).WithMany(x => x.BieuGiaCongViec_CapNgam).HasForeignKey(x => x.IdCongViec); // cấu hình 1-N công việc
            ConfigureBase(builder);
        }
    }
}