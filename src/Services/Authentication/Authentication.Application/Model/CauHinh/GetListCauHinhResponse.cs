using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.CauHinh
{
    public class GetListCauHinhResponse
    {
        public string TenPhanLoai { get; set; }
        public string TenCauHinh { get; set; }
        public string GiaTri { get; set; }
        public int? Quy { get; set; }
        public int? Nam { get; set; }
    }
}
