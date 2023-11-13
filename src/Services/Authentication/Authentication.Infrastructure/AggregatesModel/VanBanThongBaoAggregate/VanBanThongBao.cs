using EVN.Core.Models.Base;
using EVN.Core.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.ActionsAggregate
{
    public class VanBanThongBao : BaseEntity
    {
        public int Nam { get; set; }
        public int Quy { get; set; }
        public string GhiChu { get; set; }
        public string Url { get; set; }
    }
}
