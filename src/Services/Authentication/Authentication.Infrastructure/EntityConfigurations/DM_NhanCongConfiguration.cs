using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_NhanCongConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_NhanCong>
    {
        public void Configure(EntityTypeBuilder<DM_NhanCong> builder)
        {
            builder.ToTable("DM_NhanCong"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.CapBac).HasMaxLength(10); // Cấu hình độ dài tên vật liệu 
            builder.Property(x => x.HeSo).HasMaxLength(10); // Cấu hình độ dài mã  vật liệu 
            builder.HasOne(x => x.KhuVuc).WithMany(x => x.DM_NhanCong).HasForeignKey(x => x.IdKhuVuc);
            ConfigureBase(builder);
        }
    }
}