using EVN.Core.SeedWork;

namespace Authentication.Application.Model.BieuGiaCongViec
{
    public class BieuGiaCongViecRequest : PagingQuery // kế thừa PagingQuery 
    {
        public Guid? IdKhuVuc { get; set; }
        public Guid? IdLoaiBieuGia { get; set; }
        public Guid? IdBieuGia { get; set; }
        public string IdPhanLoai { get; set; }
    }
}
