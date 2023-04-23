using EVN.Core.SeedWork.ExtendEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model.DM_BieuGia
{
    public class DM_BieuGiaResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public Guid? idLoaiBieuGia { get; set; }
        public string MaBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public string TenLoaiBieuGia { get; set; }
        public string TenKhuVuc { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
