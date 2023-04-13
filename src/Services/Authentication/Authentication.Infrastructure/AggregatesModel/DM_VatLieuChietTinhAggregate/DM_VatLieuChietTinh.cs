using Authentication.Infrastructure.AggregatesModel.DonGiaChietTinhAggregate;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.DM_VatLieuChietTinhAggregate
{
    public class DM_VatLieuChietTinh : BaseEntity
    {
        public string TenVatLieuChietTinh { get; set; }
        public string MaVatLieuChietTinh { get; set; }
        public string DonViTinh { get; set; }
        public ICollection<DonGiaChietTinh> DonGiaChietTinh { get; set; }

    }
}
