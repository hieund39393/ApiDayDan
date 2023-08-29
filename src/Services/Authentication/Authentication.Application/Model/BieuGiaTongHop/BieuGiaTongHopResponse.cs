namespace Authentication.Application.Model.BieuGiaTongHop
{
    public class BieuGiaTongHopResponse
    {
        public string TenBieuGia { get; set; }
        public string DonVi { get; set; }
        public int? TinhTrang { get; set; }

        public List<string> ListData { get; set; }
    }

    public class CSKHResponse
    {
        public string TenKhuVuc { get; set; }
        public List<BGTHChiTiet> ListBieuGiaChiTiet { get; set; }
    }
    public class BGTHChiTiet
    {
        public int Stt { get; set; }
        public string TenBieuGia { get; set; }
        public string DonVi { get; set; }
        public string DonGiaCot1 { get; set; }
        public string DonGiaCot2 { get; set; }
        public string DonGiaCot3 { get; set; }
    }

    public class CSKHCapNgamResponse
    {
        public string TenKhuVuc { get; set; }
        public List<NhomVung> NhomVung { get; set; }
    }
    public class NhomVung
    {
        public string Nhom { get; set; }
        public List<BGTHChiTiet> ListBieuGiaChiTiet { get; set; }
    }

    public class BGTHReponse
    {
        public Guid IdBieuGia { get; set; }
        public string TenBieuGia { get; set; }
        public Guid? IdLoaiBieuGia { get; set; }
        public string PhanLoaiBieuGia { get; set; }
        public Guid? IdKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? DonGia2 { get; set; }
        public decimal? DonGia3 { get; set; }
        public int TinhTrang { get; set; }
    }
}
