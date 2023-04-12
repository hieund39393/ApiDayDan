using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate
{
    public class DonGiaNhanCong : BaseEntity
    {
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdVung { get; set; }
        public DM_Vung Vung { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public DM_KhuVuc KhuVuc { get; set; }
        public decimal DonGia { get; set; }
    }
}
