namespace Authentication.Application.Model.ChiTietBieuGia
{
    // class này để lấy các trường cần hiển thị

    public class ChiTietBieuGiaResult
    {
        public List<ChiTietBieuGiaResponse> ListBieuGia { get; set; }
        public decimal Tong { get; set; }
        public decimal KhaoSat { get; set; }
        public decimal CongTruocThue { get; set; }
        public decimal DonGiaTongHopTruocThue { get; set; }

        public decimal DonGiaThu5 { get; set; }
        public decimal DonGiaThu6 { get; set; }
        public decimal DonGiaThu7 { get; set; }
        public bool ChuaCoDuLieu { get; set; }

        public string CPChung { get; set; }
        public string CPChung1 { get; set; }
        public string CPChung2 { get; set; }
        public string CPChung3 { get; set; }
        public string CPNhaTam { get; set; }
        public string CPCVKXD { get; set; }
        public string TNCT { get; set; }

        public bool ChuaCoDuLieuBieuGia { get; set; }
    }
    public class ChiTietBieuGiaResponse
    {
        public string Stt { get; set; }
        public Guid? Id { get; set; }
        public Guid? IdBieuGia { get; set; }
        public string MaNoiDungCongViec { get; set; }
        public string NoiDungCongViec { get; set; }
        public string DonViTinh { get; set; }
        public Guid? IdCongViec { get; set; }
        public int? Nam { get; set; }
        public int? Quy { get; set; }
        public decimal? SoLuong { get; set; }
        public decimal? HeSoDieuChinh_K1nc { get; set; }
        public decimal? HeSoDieuChinh_K2nc { get; set; }
        public decimal? HeSoDieuChinh_Kmtc { get; set; }
        public decimal? DonGia_VL { get; set; }
        public decimal? DonGia_NC { get; set; }
        public decimal? DonGia_MTC { get; set; }

        public decimal? CPChung { get; set; }
        public decimal? CPChung2 { get; set; }
        public decimal? CPChung3 { get; set; }
        public decimal? CPNhaTam { get; set; }
        public decimal? CPCongViecKhongXDDuocTuTK { get; set; }
        public decimal? ThuNhapChiuThue { get; set; }
        public decimal? DonGiaTruocThue { get; set; }
        public decimal? GiaTriTruocThue { get; set; }
        public bool CongViecChinh { get; set; }
        public int? ThuTuHienThi { get; set; }

        public int PhanLoai { get; set; }
        public int tt { get; set; }

        public string VungKhuVuc { get; set; }

        public bool ChuaCoDuLieu { get; set; }
    }
}
