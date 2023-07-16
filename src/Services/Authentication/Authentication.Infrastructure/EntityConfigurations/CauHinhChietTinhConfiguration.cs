using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class CauHinhChietTinhConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<CauHinhChietTinh>
    {
        public void Configure(EntityTypeBuilder<CauHinhChietTinh> builder)
        {
            builder.ToTable("CauHinhChietTinh"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.HasOne(x => x.DM_CongViec).WithMany(x => x.CauHinhChietTinhs).HasForeignKey(x => x.IdCongViec); // cấu hình 1-N loại công việc

            ConfigureBase(builder);
        }
    }
}