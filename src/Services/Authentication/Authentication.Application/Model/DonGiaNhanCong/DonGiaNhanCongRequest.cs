using EVN.Core.SeedWork;

namespace Authentication.Application.Model.DonGiaNhanCong
{
    // class này để lấy các trường cần hiển thị
    public class DonGiaNhanCongRequest : PagingQuery // kế thừa PagingQuery 
    {
        public int PhanLoai { get; set; }
    }
}
