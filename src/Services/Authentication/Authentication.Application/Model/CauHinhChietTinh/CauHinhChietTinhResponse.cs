using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.CauHinhChietTinh
{
    public class CauHinhChietTinhResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid? IdCongViec { get; set; }
        public string TenCongViec { get; set; }
        public List<Guid> IdMTC { get; set; }
        public List<Guid> IdNhanCong { get; set; }
        public List<Guid> IdVatLieu { get; set; }

    }
}
