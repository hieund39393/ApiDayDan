using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuChietTinhAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class DonGiaChietTinh : BaseEntity
    {
        public Guid? IdCongViec { get; set; }
        public decimal? DonGiaVatLieu { get; set; }
        public decimal? DonGiaNhanCong { get; set; }
        public decimal? DonGiaMTC { get; set; }

        public DM_CongViec DM_CongViec { get; set; }
    }
}
