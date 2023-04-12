using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.DM_VatLieuAggregate
{
    public class DM_VatLieu : BaseEntity
    {
        public string TenVatLieu { get; set; }
        public string MaVatLieu { get; set; }
        public string DonViTinh { get; set; }
    }
}
