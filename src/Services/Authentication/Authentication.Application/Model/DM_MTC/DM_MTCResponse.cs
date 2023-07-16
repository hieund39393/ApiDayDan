using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_MTC
{
    // class này để lấy các trường cần hiển thị
    public class DM_MTCResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string TenMTC { get; set; }
        public string MaMTC { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
