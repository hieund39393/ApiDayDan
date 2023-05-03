using EVN.Core.SeedWork.ExtendEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.ChiTietBieuGia
{
    public class GetDonGiaResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public string Ten { get; set; }
        public string Ma { get; set; }
        public string DonViTinh { get; set; }
        public string DonGia { get; set; }
    }
}
