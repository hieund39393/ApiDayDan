using EVN.Core.SeedWork;

namespace Authentication.Application.Model.DM_CongViec
{
    // class này để lấy các trường cần hiển thị
    public class DM_CongViecRequest : PagingQuery // kế thừa PagingQuery 
    {
        public string TenCongViec { get; set; }
        public string MaCongViec { get; set; }
    }
}
