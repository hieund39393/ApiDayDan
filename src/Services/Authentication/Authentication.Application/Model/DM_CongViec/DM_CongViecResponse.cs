using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_CongViec
{
    // class này để lấy các trường cần hiển thị
    public class DM_CongViecResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
        public string DonViTinh { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
