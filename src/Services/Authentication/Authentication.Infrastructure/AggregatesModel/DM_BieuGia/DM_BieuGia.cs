using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.ChiTietBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.BieuGiaCongViecAggregate;

namespace AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia
{
    public class DM_BieuGia : BaseEntity
    {
        public Guid? idLoaiBieuGia { get; set; }
        public string MaBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public DM_LoaiBieuGia DM_LoaiBieuGia { get; set; }  // cấu hình 1-N bảng Loại biểu giá
        public ICollection<ChiTietBieuGia> ChiTietBieuGia { get; set; } // cấu hình 1-N bảng biểu giá công việc
        public ICollection<BieuGiaCongViec> BieuGiaCongViec { get; set; } // cấu hình 1-N bảng biểu giá công việc

    }
}
