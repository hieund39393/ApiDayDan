using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class ChietTinhChiTiet : BaseEntity
    {
        public Guid IdDonGiaChietTinh { get; set; }
        public Guid? IdCongViec { get; set; }
        public Guid? IdChiTiet { get; set; }
        public decimal? DinhMuc { get; set; }

        public int PhanLoai { get; set; }
        public DonGiaChietTinh DonGiaChietTinh { get; set; }


    }
}
