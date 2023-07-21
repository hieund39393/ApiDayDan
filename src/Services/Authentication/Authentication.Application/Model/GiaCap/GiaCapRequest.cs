using EVN.Core.SeedWork;

namespace Authentication.Application.Model.GiaCap
{
    // class này để lấy các trường cần hiển thị
    public class GiaCapRequest : PagingQuery // kế thừa PagingQuery 
    {
        public int VungKhuVuc { get; set; }
    }
}
