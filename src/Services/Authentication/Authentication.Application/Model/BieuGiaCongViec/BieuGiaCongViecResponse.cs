using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.BieuGiaCongViec
{
    public class BieuGiaCongViecResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string BieuGia { get; set; }
        public string Vung { get; set; }
        public string KhuVuc { get; set; }
        public string TenCongViec { get; set; }
    }
}
