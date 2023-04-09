using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_VungAggregate
{
    public class DM_Vung: BaseEntity
    {
        public string TenVung { get; set; }
        public string GhiChu { get; set; }
        public ICollection<ChiTietBieuGia> ChiTietBieuGia { get; set; } // cấu hình 1-N bảng biểu giá
        public ICollection<BieuGiaCongViec> BieuGiaCongViec { get; set; } // cấu hình 1-N bảng biểu giá công việc

    }
}
