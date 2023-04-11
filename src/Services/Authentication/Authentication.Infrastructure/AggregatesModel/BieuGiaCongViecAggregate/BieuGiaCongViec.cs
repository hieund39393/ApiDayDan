using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_CongViecAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_VungAggregate;
using EVN.Core.Models.Base;

namespace Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate
{
    public class BieuGiaCongViec: BaseEntity
    {
        // biểu giá
        public Guid? BieuGiaID { get; set; }
        public DM_BieuGia DM_BieuGia{ get; set; }
        
        // Vùng
        public Guid? VungID { get; set; }
        public DM_Vung DM_Vung{ get; set; }     
        
        // Khu vực
        public Guid? KhuVucID { get; set; }
        public DM_KhuVuc DM_KhuVuc{ get; set; }      
        
        // Công việc
        public Guid? CongViecID { get; set; }
        public DM_CongViec DM_CongViec { get; set; }
        //public ICollection<ChiTietBieuGia> ChiTietBieuGia { get; set; } // cấu hình 1-N bảng biểu giá


    }
}
