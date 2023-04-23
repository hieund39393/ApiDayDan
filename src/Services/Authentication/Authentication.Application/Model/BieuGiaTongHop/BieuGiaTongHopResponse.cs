using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.BieuGiaTongHop
{
    public class BieuGiaTongHopResponse
    {
        public string TenBieuGia { get; set; }
        public string DonVi { get; set; }
        public int? TinhTrang { get; set; }

        public List<string> ListData { get; set; }
    }

}
