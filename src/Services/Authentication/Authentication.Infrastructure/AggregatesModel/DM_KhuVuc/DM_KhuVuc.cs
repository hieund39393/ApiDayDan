using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_KhuVuc
{
    public class DM_KhuVuc : BaseEntity
    {
        public string TenKhuVuc { get; set; }
        public string GhiChu { get; set; }
    }
}
