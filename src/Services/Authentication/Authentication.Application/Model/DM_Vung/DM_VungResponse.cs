using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DM_Vung
{
    // class này để lấy các trường cần hiển thị
    public class DM_VungResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public string TenVung { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
