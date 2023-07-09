using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_BieuGia_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_BieuGia_CapNgam>
    {
        public void Configure(EntityTypeBuilder<DM_BieuGia_CapNgam> builder)
        {
            builder.ToTable("DM_BieuGia_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenBieuGia).HasMaxLength(200); // Cấu hình độ dài tên biểu giá
            builder.Property(x => x.MaBieuGia).HasMaxLength(20); // Cấu hình độ dài mã biểu giá
             builder.HasOne(x => x.DM_LoaiBieuGia_CapNgam).WithMany(x => x.DM_BieuGia_CapNgam).HasForeignKey(x => x.idLoaiBieuGia); // cấu hình 1-N loại biểu giá

            ConfigureBase(builder);
        }
    }
}