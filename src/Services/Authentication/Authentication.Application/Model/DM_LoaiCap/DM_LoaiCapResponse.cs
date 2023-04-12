using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_LoaiCap
{
    // class này để lấy các trường cần hiển thị
    public class DM_LoaiCapResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string TenLoaiCap { get; set; }
        public string MaLoaiCap { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
