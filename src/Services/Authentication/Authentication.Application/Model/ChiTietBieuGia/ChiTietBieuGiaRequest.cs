namespace Authentication.Application.Model.ChiTietBieuGia
{
    // class này để lấy các trường cần hiển thị
    public class ChiTietBieuGiaRequest
    {
        public int Quy { get; set; }
        public int Nam { get; set; }
        public Guid IDBieuGia { get; set; }
    }
}
