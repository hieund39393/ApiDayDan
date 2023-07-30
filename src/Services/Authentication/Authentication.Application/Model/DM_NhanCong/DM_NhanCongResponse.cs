using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DonGiaNhanCong
{
    // class này để lấy các trường cần hiển thị
    public class DM_NhanCongResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid   Id { get; set; }
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public string NgayTao{ get;set; }
        public string VungKhuVuc { get; set; }
        public int PhanLoai { get; set; }
    }
}
