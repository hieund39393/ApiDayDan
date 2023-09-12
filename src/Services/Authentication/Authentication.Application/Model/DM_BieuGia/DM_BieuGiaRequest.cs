using EVN.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.DM_BieuGia
{
    public class DM_BieuGiaRequest : PagingQuery // kế thừa PagingQuery 
    {
        public Guid? IdKhuVuc { get; set; }
        public Guid? IdLoaiBieuGia { get; set; }
    }
}
