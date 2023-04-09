using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate
{
    public class ChiTietBieuGia : BaseEntity
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int SoLuong { get; set; }

        //loại biểu giá
        public Guid? BieugiaID { get; set; }
        public DM_BieuGia DM_BieuGia { get; set; }
        //vùng
        public Guid? VungID { get; set; }
        public DM_Vung DM_Vung { get; set; }
        // Công việc
        public Guid? CongViecID { get; set; }
        public DM_CongViec DM_CongViec { get; set; }
        // Khu vực
        public Guid? KhuVucID { get; set; }
        public DM_KhuVuc DM_KhuVuc { get; set; }

        public float HeSoDieuChinh_K1nc { get; set; }
        public float HeSoDieuChinh_K2nc { get; set; }
        public float HeSoDieuChinh_K2mnc { get; set; }
        public float DonGia_VL { get; set; }
        public float DonGia_NC { get; set; }
        public float DonGia_MTC { get; set; }

    }
}
