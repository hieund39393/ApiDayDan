using EVN.Core.SeedWork;

namespace Authentication.Application.Model.DM_LoaiBieuGia
{
    // class này để lấy các trường cần hiển thị
    public class DM_LoaiBieuGiaRequest : PagingQuery // kế thừa PagingQuery 
    {
        public string TenBieuGia { get; set; }
        public string MaBieuGia { get; set; }
    }
}
