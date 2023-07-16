using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DonGiaMTC
{
    // class này để lấy các trường cần hiển thị
    public class DonGiaMTCResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public Guid? IdMTC { get; set; }
        public string TenMTC { get; set; }
        public string DonViTinh { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia{ get; set; }
        public decimal? DinhMuc { get; set; }
        public string NgayTao { get; set; }
    }
}
