using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate
{
    public class CauHinhChietTinh_CapNgam : BaseEntity
    {

        public int ThuTuHienThi { get; set; }
        public Guid? IdCongViec { get; set; }

        public Guid? IdChiTiet { get; set; }

        public int PhanLoai { get; set; }

        public DM_CongViec_CapNgam DM_CongViec_CapNgam { get; set; }
        public int VungKhuVuc { get; set; }
    }
}
