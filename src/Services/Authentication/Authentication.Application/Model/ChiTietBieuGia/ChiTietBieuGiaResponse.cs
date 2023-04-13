namespace Authentication.Application.Model.ChiTietBieuGia
{
    // class này để lấy các trường cần hiển thị
    public class ChiTietBieuGiaResponse
    {
        public Guid Id { get; set; }
        public Guid? IDBieuGia { get; set; }
        public string MaNoiDungCongViec { get; set; }
        public string NoiDungCongViec { get; set; }
        public string DonViTinh { get; set; }
        public Guid? IDCongViec { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public decimal SoLuong { get; set; }
        public decimal HeSoDieuChinh_K1nc { get; set; }
        public decimal HeSoDieuChinh_K2nc { get; set; }
        public decimal HeSoDieuChinh_K2mnc { get; set; }
        public decimal DonGia_VL { get; set; }
        public decimal DonGia_NC { get; set; }
        public decimal DonGia_MTC { get; set; }

        public decimal CPChung { get; set; }
        public decimal CPNhaTam { get; set; }
        public decimal CPCongViecKhongXDDuocTuTK { get; set; }
        public decimal ThuNhapChiuThue { get; set; }
        public decimal DonGiaTruocThue { get; set; }
        public decimal GiaTriTruocThue { get; set; }



        public DateTime NgayTao { get; set; }
    }
}
