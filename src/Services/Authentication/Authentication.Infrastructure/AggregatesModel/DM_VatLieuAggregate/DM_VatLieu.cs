using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate
{
    public class DM_VatLieu : BaseEntity
    {
        public int ThuTuHienThi { get; set; }
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string DonViTinh { get; set; }
        public ICollection<DonGiaVatLieu> DonGiaVatLieu { get; set; }
    }
}
