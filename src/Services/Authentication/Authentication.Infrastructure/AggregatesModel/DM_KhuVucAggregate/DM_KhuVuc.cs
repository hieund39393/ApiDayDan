using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate
{
    public class DM_KhuVuc : BaseEntity
    {
        public int PhanLoai { get; set; }
        public string TenKhuVuc { get; set; }
        public string GhiChu { get; set; }
        public ICollection<DM_LoaiBieuGia> DM_LoaiBieuGia { get; set; } // cấu hình 1-N bảng loại biểu giá
        public ICollection<DM_NhanCong> DM_NhanCong { get; set; } // cấu hình 1-N bảng loại biểu giá

        public ICollection<DM_LoaiBieuGia_CapNgam> DM_LoaiBieuGia_CapNgam { get; set; } // cấu hình 1-N bảng loại biểu giá
        public ICollection<DM_NhanCong_CapNgam> DM_NhanCong_CapNgam { get; set; } // cấu hình 1-N bảng loại biểu giá


    }
}
