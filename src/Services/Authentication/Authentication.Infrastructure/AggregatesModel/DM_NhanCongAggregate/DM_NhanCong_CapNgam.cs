using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using Authentication.Infrastructure.AggregatesModel.DonGiaNhanCongAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_NhanCongAggregate
{
    public class DM_NhanCong_CapNgam : BaseEntity
    {
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public DM_KhuVuc KhuVuc { get; set; }
        public ICollection<DonGiaNhanCong_CapNgam> DonGiaNhanCong_CapNgam { get; set; }
    }
}
