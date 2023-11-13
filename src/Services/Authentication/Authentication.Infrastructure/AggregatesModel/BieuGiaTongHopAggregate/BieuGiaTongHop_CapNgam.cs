using AAuthentication.Infrastructure.AggregatesModel.DM_BieuGia;
using EVN.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Infrastructure.AggregatesModel.BieuGiaTongHopAggregate
{
    public class BieuGiaTongHop_CapNgam : BaseEntity
    {
        public DM_BieuGia_CapNgam DM_BieuGia_CapNgam { get; set; }
        public Guid IdBieuGia { get; set; }
        public int Quy { get; set; }
        public int Nam { get; set; }
        public decimal DonGia { get; set; }
        public decimal DonGia2 { get; set; }
        public decimal DonGia3 { get; set; }
        public int TinhTrang { get; set; }
        public DateTime? NgayXacNhan { get; set; }
        public Guid? NguoiXacNhan { get; set; }
        public DateTime? NgayHieuLuc { get; set; }

    }
}
