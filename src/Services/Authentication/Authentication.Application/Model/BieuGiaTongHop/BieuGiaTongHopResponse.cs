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
}
