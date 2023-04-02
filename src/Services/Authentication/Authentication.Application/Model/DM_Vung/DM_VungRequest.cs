using EVN.Core.SeedWork;

namespace Authentication.Application.Model.DM_Vung
{
    // class này để lấy các trường cần hiển thị
    public class DM_VungRequest : PagingQuery // kế thừa PagingQuery 
    {
        public string TenVung { get; set; }
        public string GhiChu { get; set; }
    }
}
