using EVN.Core.Models.Base;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate;

namespace AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia
{
    public class DM_BieuGia_CapNgam : BaseEntity
    {
        public Guid? idLoaiBieuGia { get; set; }
        public string MaBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public DM_LoaiBieuGia_CapNgam DM_LoaiBieuGia_CapNgam { get; set; }  // cấu hình 1-N bảng Loại biểu giá
        public ICollection<ChiTietBieuGia_CapNgam> ChiTietBieuGia_CapNgam { get; set; } // cấu hình 1-N bảng biểu giá công việc
        public ICollection<BieuGiaCongViec_CapNgam> BieuGiaCongViec_CapNgam { get; set; } // cấu hình 1-N bảng biểu giá công việc

        public ICollection<BieuGiaTongHop_CapNgam> BieuGiaTongHop_CapNgam { get; set; }
    }
}
