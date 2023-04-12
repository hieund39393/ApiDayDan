using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_VatLieu
{
    // class này để lấy các trường cần hiển thị
    public class DM_VatLieuResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
