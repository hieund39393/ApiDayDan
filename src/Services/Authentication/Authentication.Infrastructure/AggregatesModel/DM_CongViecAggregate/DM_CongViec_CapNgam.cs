using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate
{
    public class DM_CongViec_CapNgam : BaseEntity
    {
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
        public string DonViTinh { get; set; }
        public ICollection<BieuGiaCongViec_CapNgam> BieuGiaCongViec_CapNgam { get; set; } // cấu hình 1-N bảng biểu giá công việc
        public ICollection<ChiTietBieuGia_CapNgam> ChiTietBieuGia_CapNgam { get; set; } // cấu hình 1-N bảng biểu giá công việc

        public ICollection<DonGiaChietTinh_CapNgam> DonGiaChietTinh_CapNgams { get; set; }
    }
}
