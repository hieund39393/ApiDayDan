using EVN.Core.SeedWork;

namespace Authentication.Application.Model.CauHinhChietTinh
{
    public class CauHinhChietTinhRequest : PagingQuery // kế thừa PagingQuery 
    {
        public Guid? IdKhuVuc { get; set; }
        public Guid? IdLoaiBieuGia { get; set; }
        public Guid? IdBieuGia { get; set; }
        public string IdPhanLoai { get; set; }
        public int VungKhuVuc { get; set; }

    }
}
