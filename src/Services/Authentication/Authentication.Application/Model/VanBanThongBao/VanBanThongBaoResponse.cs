using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.CauHinh
{
    public class VanBanThongBaoResponse
    {
        public Guid Id{ get; set; }
        public int Quy { get; set; }
        public int Nam { get; set; }
        public string GhiChu { get; set; }
        public string Url { get; set; }
    }
}
