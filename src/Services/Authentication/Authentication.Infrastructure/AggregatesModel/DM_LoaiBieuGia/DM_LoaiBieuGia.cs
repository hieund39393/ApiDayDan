using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGia
{
    public class DM_LoaiBieuGia : BaseEntity
    {
        public string MaBieuGia { get; set; }
        public string TenBieuGia { get; set; }
    }
}
