using Authentication.Infrastructure.AggregatesModel.DonGiaVatLieuAggregate;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.DM_MTCAggregate
{
    public class DM_MTC : BaseEntity
    {
        public string TenMayThiCong { get; set; }
        public string MaMTC { get; set; }
        public string DonViTinh { get; set; }
        public ICollection<DonGiaMTC> DonGiaMTC { get; set; }
    }
}
