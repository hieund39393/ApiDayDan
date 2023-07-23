using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate
{
    public class DM_CongViec : BaseEntity
    {
        public int ThuTuHienThi { get; set; } 
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
        public string DonViTinh { get; set; }
        public ICollection<BieuGiaCongViec> BieuGiaCongViec { get; set; } // cấu hình 1-N bảng biểu giá công việc
        public ICollection<ChiTietBieuGia> ChiTietBieuGia { get; set; } // cấu hình 1-N bảng biểu giá công việc
        public ICollection<CauHinhChietTinh> CauHinhChietTinhs { get; set; } 

        public ICollection<DonGiaChietTinh> DonGiaChietTinhs { get; set; }
    }
}
