using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class DonGiaChietTinh_CapNgam : BaseEntity
    {
        public Guid? IdCongViec { get; set; }
        public decimal? DonGiaVatLieu { get; set; }
        public decimal? DonGiaNhanCong { get; set; }
        public decimal? DonGiaMTC { get; set; }

        public DM_CongViec_CapNgam DM_CongViec_CapNgam { get; set; }
        public int VungKhuVuc { get; set; }

        public ICollection<ChietTinhChiTiet_CapNgam> ChietTinhChiTiet_CapNgams { get; set; }
    }
}
