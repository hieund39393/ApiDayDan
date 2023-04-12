using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate
{
    public class DonGiaVatLieu: BaseEntity
    {
        public Guid? IdVatLieu { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public DM_VatLieu DM_VatLieu { get; set; }
    }
}
