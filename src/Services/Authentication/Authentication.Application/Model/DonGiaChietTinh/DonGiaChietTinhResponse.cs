using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DonGiaChietTinh
{
    // class này để lấy các trường cần hiển thị
    public class DonGiaChietTinhResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public Guid? IdVatLieu { get; set; }
        public string TenVatLieu { get; set; }
        public decimal DonGia { get; set; }
        public string PhanLoai{ get; set; }
        public int IdPhanLoai{ get; set; }
        public DateTime NgayTao { get; set; }
    }
}
