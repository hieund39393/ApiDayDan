using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate
{
    public class ChiTietBieuGia_CapNgam : BaseEntity
    {
        public Guid? IDBieuGia { get; set; }
        public Guid? IDCongViec { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public decimal SoLuong { get; set; }
        public decimal HeSoDieuChinh_K1nc { get; set; }
        public decimal HeSoDieuChinh_K2nc { get; set; }
        public decimal HeSoDieuChinh_Kmtc { get; set; }
        public decimal DonGia_VL { get; set; }
        public decimal DonGia_NC { get; set; }
        public decimal DonGia_MTC { get; set; }
        public DM_BieuGia_CapNgam DM_BieuGia_CapNgam { get; set; }
        public DM_CongViec_CapNgam DM_CongViec_CapNgam { get; set; }


    }
}
