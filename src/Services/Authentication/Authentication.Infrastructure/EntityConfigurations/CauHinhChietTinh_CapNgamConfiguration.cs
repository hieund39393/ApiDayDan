using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class CauHinhChietTinh_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<CauHinhChietTinh_CapNgam>
    {
        public void Configure(EntityTypeBuilder<CauHinhChietTinh_CapNgam> builder)
        {
            builder.ToTable("CauHinhChietTinh_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.HasOne(x => x.DM_CongViec_CapNgam).WithMany(x => x.CauHinhChietTinh_CapNgams).HasForeignKey(x => x.IdCongViec); // cấu hình 1-N loại công việc

            ConfigureBase(builder);
        }
    }
}