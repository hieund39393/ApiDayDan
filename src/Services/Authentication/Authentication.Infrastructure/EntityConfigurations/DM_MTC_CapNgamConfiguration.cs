using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_MTC_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_MTC_CapNgam>
    {
        public void Configure(EntityTypeBuilder<DM_MTC_CapNgam> builder)
        {
            builder.ToTable("DM_MTC_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenMTC).HasMaxLength(200); // Cấu hình độ dài tên vật liệu 
            builder.Property(x => x.MaMTC).HasMaxLength(50); // Cấu hình độ dài mã  vật liệu 
            builder.Property(x => x.DonViTinh).HasMaxLength(50); // Cấu hình độ dài đơn vị tính
            ConfigureBase(builder);
        }
    }
}