using Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate
{
    public class DonGiaMTC_CapNgam : BaseEntity
    {
        public Guid? IdMTC { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DonGiaCu { get; set; }

        public decimal? DinhMuc { get; set; }
        public decimal? DinhMucCu { get; set; }
        public DM_MTC_CapNgam DM_MTC_CapNgam { get; set; }
        public int VungKhuVuc { get; set; }
    }
}
