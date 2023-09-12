using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.BieuGiaTongHop
{
    public class ApiDonGiaVatLieuResponse
    {
        public string MaVatTu { get; set; }
        public string NgayHieuLuc { get; set; }
        public ApiDonGia DonGiaTronGoi { get; set; }
        public ApiDonGia DonGiaTuTucCapSau { get; set; }
        public ApiDonGia DonGiaTuTucCapVaVatTu { get; set; }
        public string HinhThucThiCong { get; set; }
        public string? DonGiaNhanCong { get; set; }
    }

    public class ApiDonGia
    {
        public string CapDien { get; set; }
        public string NangCongSuat { get; set; }
        public string DiDoi { get; set; }
    }
}
