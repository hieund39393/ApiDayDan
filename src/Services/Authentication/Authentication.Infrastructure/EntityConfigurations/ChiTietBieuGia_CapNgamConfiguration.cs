using Authentication.Infrastructure.EF.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;

namespace Authentication.Infrastructure.EntityConfigurations
{
    public class ChiTietBieuGia_CapNgamConfiguration : CommonPropertyConfiguration, IEntityTypeConfiguration<ChiTietBieuGia_CapNgam>
    {
        public void Configure(EntityTypeBuilder<ChiTietBieuGia_CapNgam> builder)
        {
            builder.ToTable("ChiTietBieuGia_CapNgam"); // tên bảng
            builder.HasKey(x => new { x.Id }); // Cấu hình Khoá chính
            builder.Property(x => x.SoLuong).HasColumnType("numeric(18,2)"); ;
            builder.Property(x => x.HeSoDieuChinh_K1nc).HasColumnType("numeric(18,2)");
            builder.Property(x => x.HeSoDieuChinh_K2nc).HasColumnType("numeric(18,2)");
            builder.Property(x => x.HeSoDieuChinh_Kmtc).HasColumnType("numeric(18,2)");
            builder.Property(x => x.DonGia_VL).HasColumnType("numeric(18,2)");
            builder.Property(x => x.DonGia_NC).HasColumnType("numeric(18,2)");
            builder.Property(x => x.DonGia_MTC).HasColumnType("numeric(18,2)");
            builder.HasOne(x => x.DM_BieuGia_CapNgam).WithMany(x => x.ChiTietBieuGia_CapNgam).HasForeignKey(x => x.IDBieuGia); // cấu hình 1-N loại biểu giá
            builder.HasOne(x => x.DM_CongViec_CapNgam).WithMany(x => x.ChiTietBieuGia_CapNgam).HasForeignKey(x => x.IDCongViec); // cấu hình 1-N loại công việc

            ConfigureBase(builder);
        }
    }
}
