using EVN.Core.SeedWork;

namespace Authentication.Application.Model.DM_KhuVuc
{
    // class này để lấy các trường cần hiển thị
    public class DM_KhuVucRequest : PagingQuery // kế thừa PagingQuery 
    {
        public string TenKhuVuc { get; set; }
        public string GhiChu { get; set; }
    }
}
