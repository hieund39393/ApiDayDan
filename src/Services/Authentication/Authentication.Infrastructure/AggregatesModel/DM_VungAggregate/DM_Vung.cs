using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_VungAggregate
{
    public class DM_Vung: BaseEntity
    {
        public string TenVung { get; set; }
        public string GhiChu { get; set; }
        public ICollection<DM_LoaiBieuGia> DM_LoaiBieuGia { get; set; } // cấu hình 1-N bảng loại biểu giá
        public ICollection<DonGiaNhanCong> DonGiaNhanCong { get; set; } // cấu hình 1-N bảng loại biểu giá


    }
}
