using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.BieuGiaTongHop
{
    public class ApiDonGiaVatLieuResponse
    {
        public ApiDonGia DonGiaTronGoi { get; set; }
        public ApiDonGia DonGiaTuTucCapSau { get; set; }
        public ApiDonGia DonGiaTuTucCapVaVatTu { get; set; }
    }

    public class ApiDonGia
    {
        public string CapDien { get; set; }
        public string NangCongSuat { get; set; }
        public string DiDoi { get; set; }
    }
}
