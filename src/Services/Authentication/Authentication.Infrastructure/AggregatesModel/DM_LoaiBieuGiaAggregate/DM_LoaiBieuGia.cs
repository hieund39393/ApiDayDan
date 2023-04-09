using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate
{
    public class DM_LoaiBieuGia : BaseEntity
    {
        public string MaBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public ICollection<DM_BieuGia> DM_BieuGia { get; set; } // cấu hình 1-N bảng biểu giá


    }
}
