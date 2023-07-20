using Authentication.Infrastructure.AggregatesModel.GiaCapAggregate;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.DM_LoaiCapAggregate
{
    public class DM_LoaiCap_CapNgam : BaseEntity
    {
        public string TenLoaiCap { get; set; }
        public string MaLoaiCap { get; set; }
        public string DonViTinh { get; set; }
        public ICollection<GiaCap_CapNgam> GiaCap_CapNgam { get; set; }
        public int VungKhuVuc { get; set; }
    }
}
