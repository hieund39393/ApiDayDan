using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate
{
    public class DM_LoaiBieuGia : BaseEntity
    {
        public string MaLoaiBieuGia { get; set; }
        public string TenLoaiBieuGia { get; set; }
        //Vùng
        public Guid? VungID { get; set; }
        public DM_Vung DM_Vung { get; set; }
        //Khu vực
        public Guid? KhuVucID { get; set; }
        public DM_KhuVuc DM_KhuVuc{ get; set; }

        public ICollection<DM_BieuGia> DM_BieuGia { get; set; } // cấu hình 1-N bảng biểu giá

    }
}
