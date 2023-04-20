using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.ChiTietBieuGia
{
    public class UpdateChiTietBieuGiaRequest
    {
        public Guid? Id { get; set; }
        public int Stt { get; set; }
        public string Ma { get; set; }
        public Guid IdBieuGia { get; set; }
        public Guid IdCongViec { get; set; }
        public string NoiDungCongViec { get; set; }
        public string DonVi { get; set; }
        public decimal SoLuong { get; set; }
        public decimal HeSoDieuChinh_K1nc { get; set; }
        public decimal HeSoDieuChinh_K2nc { get; set; }
        public decimal HeSoDieuChinh_Kmtc { get; set; }
        public decimal DonGia_VL { get; set; }
        public decimal DonGia_NC { get; set; }
        public decimal DonGia_MTC { get; set; }
        public int Quy { get; set; }
        public int Nam { get; set; }
        public bool CongViecChinh { get; set; }

    }
}
