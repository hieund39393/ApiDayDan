using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate
{
    public class BieuGiaTongHopChiTiet_CapNgam : BaseEntity
    {
        public int Quy { get; set; }
        public int Nam { get; set; }
        public int TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string VanBan { get; set; }
    }
}
