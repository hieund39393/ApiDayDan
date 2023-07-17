using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class DonGiaNhanCongConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<DonGiaNhanCong>
    {
        public void Configure(EntityTypeBuilder<DonGiaNhanCong> builder)
        {
            builder.ToTable("DonGiaNhanCong"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.DonGia).HasColumnType("numeric(18,1)"); // Cấu hình độ dài đơn giá
            builder.HasOne(x => x.NhanCong).WithMany(x => x.DonGiaNhanCong).HasForeignKey(x => x.IdNhanCong);

            ConfigureBase(builder);
        }
    }
}