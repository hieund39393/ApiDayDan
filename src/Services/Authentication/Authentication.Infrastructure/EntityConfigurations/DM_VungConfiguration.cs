using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DM_VungConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DM_Vung>
    {
        public void Configure(EntityTypeBuilder<DM_Vung> builder)
        {
            builder.ToTable("DM_Vung"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.TenVung).HasMaxLength(20); // Cấu hình độ dài tên
            builder.Property(x => x.GhiChu).HasMaxLength(200); // Cấu hình độ dài ghi chú
            ConfigureBase(builder);
        }
    }
}