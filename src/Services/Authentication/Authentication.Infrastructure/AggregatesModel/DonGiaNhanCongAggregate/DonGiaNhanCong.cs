using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate
{
    public class DonGiaNhanCong : BaseEntity
    {
     
        public Guid? IdNhanCong { get; set; }
        public DM_NhanCong NhanCong { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DonGiaCu { get; set; }

        public decimal? DinhMuc { get; set; }
        public decimal? DinhMucCu { get; set; }
    }
}
