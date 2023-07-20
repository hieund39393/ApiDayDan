using Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.GiaCapAggregate
{
    public class GiaCap_CapNgam : BaseEntity
    {
        public Guid? IdLoaiCap { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public DM_LoaiCap_CapNgam DM_LoaiCap_CapNgam { get; set; }
        public int VungKhuVuc { get; set; }
    }
}
