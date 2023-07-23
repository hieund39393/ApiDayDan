using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class CauHinhChietTinh : BaseEntity
    {

        public int ThuTuHienThi { get; set; }
        public Guid? IdCongViec { get; set; }

        public Guid? IdChiTiet { get; set; }

        public int PhanLoai { get; set; }

        public DM_CongViec DM_CongViec { get; set; }    
    }
}
