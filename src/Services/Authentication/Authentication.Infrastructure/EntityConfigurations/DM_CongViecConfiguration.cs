using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_CongViec;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_CongViecConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_CongViec>
    {
        public void Configure(EntityTypeBuilder<DM_CongViec> builder)
        {
            builder.ToTable("DM_CongViec"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenCongViec).HasMaxLength(200); // Cấu hình độ dài tên 
            builder.Property(x => x.MaCongViec).HasMaxLength(20); // Cấu hình độ dài mã biểu giá
            ConfigureBase(builder);
        }
    }
}