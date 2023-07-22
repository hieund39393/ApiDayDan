using Authentication.Infrastructure.Migrations;
using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.GiaCap
{
    // class này để lấy các trường cần hiển thị
    public class GiaCapResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public Guid? IdLoaiCap { get; set; }
        public string TenLoaiCap { get; set; }
        public string DonViTinh { get; set; }
        public string VanBan { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgayTao { get; set; }
        public string TenVungKhuVuc { get; set; }
        public string VungKhuVuc { get; set; }
    }
}
