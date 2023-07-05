using EVN.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.ChiTietBieuGia
{
    public class GetDonGiaRequest : PagingQuery
    {
        public int Nguon { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
    }
}
