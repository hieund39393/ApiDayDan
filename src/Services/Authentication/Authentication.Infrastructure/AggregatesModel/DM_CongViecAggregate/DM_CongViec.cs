using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate
{
    public class DM_CongViec : BaseEntity
    {
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
        public ICollection<ChiTietBieuGia> ChiTietBieuGia { get; set; } // cấu hình 1-N bảng biểu giá
        public ICollection<BieuGiaCongViec> BieuGiaCongViec { get; set; } // cấu hình 1-N bảng biểu giá công việc


    }
}
