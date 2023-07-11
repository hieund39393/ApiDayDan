using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.CauHinhAggregate
{
    public class CauHinhBieuGia : BaseEntity
    {
        public string TenCauHinh { get; set; }
        public int PhanLoaiCap { get; set; }
        public string NoiDung { get; set; }
        public string GiaTri { get; set; }
        public int Quy { get; set; }
        public int Nam { get; set; }
    }
}
