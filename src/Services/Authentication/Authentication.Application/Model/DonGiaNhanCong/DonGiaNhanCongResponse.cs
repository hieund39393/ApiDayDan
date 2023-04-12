using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DonGiaNhanCong
{
    // class này để lấy các trường cần hiển thị
    public class DonGiaNhanCongResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid   Id { get; set; }
        public string CapBac { get; set; }
        public string HeSo { get; set; }
        public Guid? IdVung { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public decimal DonGia { get; set; }
        public string VungKhuVuc { get;set; }
        public DateTime NgayTao{ get;set; }
    }
}
