using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.ActionsAggregate;
using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Authentication.Infrastructure.AggregatesModel.CauHinhAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class CauHinhBieuGiaConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<CauHinhBieuGia>
    {
        public void Configure(EntityTypeBuilder<CauHinhBieuGia> builder)
        {
            builder.ToTable("CauHinhBieuGia"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.GiaTri).HasMaxLength(50); // Cấu hình độ dài tên biểu giá
            builder.Property(x => x.NoiDung).HasMaxLength(200); // Cấu hình độ dài mã biểu giá
            builder.Property(x => x.TenCauHinh).HasMaxLength(200); // Cấu hình độ dài mã biểu giá

            ConfigureBase(builder);
        }
    }
}
