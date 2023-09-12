using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate
{
    public class DM_LoaiBieuGia_CapNgam : BaseEntity
    {
        public string MaLoaiBieuGia { get; set; }
        public string Code { get; set; }
        public string TenLoaiBieuGia { get; set; }
        //Vùng Khu vực
        public Guid? IdKhuVuc { get; set; }
        public DM_KhuVuc DM_KhuVuc{ get; set; }

        public ICollection<DM_BieuGia_CapNgam> DM_BieuGia_CapNgam { get; set; } // cấu hình 1-N bảng biểu giá

    }
}
