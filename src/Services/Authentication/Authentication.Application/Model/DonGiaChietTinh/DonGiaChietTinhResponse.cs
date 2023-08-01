using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.DonGiaChietTinh
{
    // class này để lấy các trường cần hiển thị
    public class DonGiaChietTinhResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public int? STT { get; set; }
        public Guid IdCongViec { get; set; }
        public string Ma { get; set; }
        public string TenVatLieu { get; set; }
        public Guid? IdVatLieu { get; set; }
        public string BacTho { get; set; }
        public string DonVi { get; set; }
        public decimal? DinhMuc { get; set; }

        public decimal? DGVL { get; set; }
        public decimal? DGNC { get; set; }
        public decimal? DGMTC { get; set; }

        public decimal? TongGia_VL { get; set; }
        public decimal? TongGia_NC { get; set; }
        public decimal? TongGia_MTC { get; set; }

        public int Level { get; set; }
        public int PhanLoai { get; set; }

        public int tt { get; set; }

        public bool IsDinhMucCu { get; set; }
        public bool IsDonGiaCu { get; set; }

        public int VungKhuVuc { get; set; }
    }

    public class DonGiaChietTinhCapNgamResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public int? STT { get; set; }
        public Guid IdCongViec { get; set; }
        public string Ma { get; set; }
        public string TenVatLieu { get; set; }
        public Guid? IdVatLieu { get; set; }
        public string BacTho { get; set; }
        public string DonVi { get; set; }
        public decimal DinhMuc { get; set; }

        public decimal? DGVL { get; set; }
        public decimal? DGNC { get; set; }
        public decimal? DGMTC { get; set; }

        public decimal? TongGia_VL { get; set; }
        public decimal? TongGia_NC { get; set; }
        public decimal? TongGia_MTC { get; set; }

        public int Level { get; set; }
        public int PhanLoai { get; set; }

        public int tt { get; set; }

        public bool IsDinhMucCu { get; set; }
        public bool IsDonGiaCu { get; set; }

        public int VungKhuVuc { get; set; }
    }
}
