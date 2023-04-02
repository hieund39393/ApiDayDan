using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_Vung
{
    public class DM_Vung: BaseEntity
    {
        public string TenVung { get; set; }
        public string GhiChu { get; set; }
    }
}
