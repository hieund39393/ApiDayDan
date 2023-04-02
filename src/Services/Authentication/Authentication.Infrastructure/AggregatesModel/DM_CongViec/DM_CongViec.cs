using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_CongViec
{
    public class DM_CongViec : BaseEntity
    {
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
    }
}
