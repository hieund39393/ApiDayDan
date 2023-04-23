using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_LoaiBieuGia
{
    // class này để lấy các trường cần hiển thị
    public class DM_LoaiBieuGiaResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string MaLoaiBieuGia { get; set; }
        public string TenLoaiBieuGia { get; set; }
        public Guid? KhuVucID { get; set; }
        public string TenKhuVuc { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
