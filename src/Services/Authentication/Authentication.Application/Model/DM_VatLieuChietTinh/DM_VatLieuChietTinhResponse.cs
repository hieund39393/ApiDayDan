using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_VatLieuChietTinh
{
    // class này để lấy các trường cần hiển thị
    public class DM_VatLieuChietTinhResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string TenVatLieuChietTinh { get; set; }
        public string MaVatLieuChietTinh { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
