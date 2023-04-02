using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_Vung;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVuc;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_KhuVucConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_KhuVuc>
    {
        public void Configure(EntityTypeBuilder<DM_KhuVuc> builder)
        {
            builder.ToTable("DM_KhuVuc"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenKhuVuc).HasMaxLength(20); // Cấu hình độ dài tên
            builder.Property(x => x.GhiChu).HasMaxLength(200); // Cấu hình độ dài ghi chú
            ConfigureBase(builder);
        }
    }
}