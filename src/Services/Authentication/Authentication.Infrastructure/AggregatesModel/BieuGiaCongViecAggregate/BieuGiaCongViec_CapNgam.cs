using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate
{
    public class BieuGiaCongViec_CapNgam : BaseEntity
    {
        // biểu giá
        public Guid? IdBieuGia { get; set; }
        public DM_BieuGia_CapNgam DM_BieuGia_CapNgam { get; set; }

        // Công việc
        public Guid? IdCongViec { get; set; }
        public DM_CongViec_CapNgam DM_CongViec_CapNgam { get; set; }

        public bool CongViecChinh { get; set; }
        public int ThuTuHienThi { get; set; }

        public int PhanLoai { get; set; }
    }
}
