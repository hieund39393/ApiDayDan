using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DonGiaNhanCong
{
    // class này để lấy các trường cần hiển thị
    public class DonGiaNhanCongResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public Guid IdNhanCong { get; set; }
        public string NhanCong { get; set; }
        public decimal DonGia { get; set; }
        public decimal? DinhMuc { get; set; }
        public string NgayTao { get; set; }

        public string VungKhuVuc { get; set; }
        public string TenVungKhuVuc { get; set; }

        public int PhanLoai { get; set; }
        public string Nhom{ get; set; }
    }
}
