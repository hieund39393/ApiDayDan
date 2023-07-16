using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate
{
    public class DonGiaVatLieu_CapNgam : BaseEntity
    {
        public Guid? IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DonGiaCu { get; set; }

        public decimal? DinhMuc { get; set; }
        public decimal? DinhMucCu { get; set; }
        public DM_VatLieu_CapNgam DM_VatLieu_CapNgam { get; set; }
    }
}
