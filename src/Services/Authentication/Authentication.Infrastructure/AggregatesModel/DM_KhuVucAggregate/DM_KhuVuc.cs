using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate
{
    public class DM_KhuVuc : BaseEntity
    {
        public string TenKhuVuc { get; set; }
        public string GhiChu { get; set; }
        public ICollection<BieuGiaCongViec> BieuGiaCongViec { get; set; } // cấu hình 1-N bảng biểu giá công việc


    }
}
