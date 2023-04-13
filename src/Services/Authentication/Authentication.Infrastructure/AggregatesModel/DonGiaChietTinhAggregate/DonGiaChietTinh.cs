using Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VatLieuChietTinhAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class DonGiaChietTinh : BaseEntity
    {
        public Guid? IdVatLieuChietTinh { get; set; }
        public DM_VatLieuChietTinh DM_VatLieuChietTinh { get; set; }
        public int IdPhanLoai { get; set; }
        public decimal DonGia{ get; set; }
        public decimal TongGia{ get; set; }
    }
}
