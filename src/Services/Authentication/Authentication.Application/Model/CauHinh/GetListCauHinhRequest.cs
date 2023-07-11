using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.CauHinh
{
    public class GetListCauHinhRequest
    {
        public int? PhanLoai { get; set; }
        public string TenCauHinh { get; set; }
        public int? Quy { get; set; }
        public int? Nam { get; set; }
    }
}
