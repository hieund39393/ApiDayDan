using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class DonGiaChietTinh : BaseEntity
    {
        public Guid? IdVatLieu { get; set; }
        public DM_VatLieu DM_VatLieu { get; set; }
        public decimal DonGia{ get; set; }
    }
}
