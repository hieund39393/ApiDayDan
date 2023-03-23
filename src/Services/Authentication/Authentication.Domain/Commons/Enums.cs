
using System.ComponentModel;

namespace ExOne.Domain.Commons
{
    public static class Enums
    {
        public enum ChucVuEnum
        {
            [Description("Quản trị viên")]
            Administrator = 0,

            #region Tổng công ty
            [Description("Tổng giám đốc")]
            TongGiamDoc = 1,
            [Description("Phó tổng giám đốc")]
            PhoTongGiamDoc = 2,
            [Description("Trưởng ban kỹ thuật")]
            TruongBanKyThuat = 3,
            [Description("Phó trưởng ban kỹ thuật")]
            PhoTruongBanKyThuat = 4,
            [Description("Chuyên viên")]
            ChuyenVien = 5,
            #endregion

            #region Đơn vị điện lực
            [Description("Giám đốc")]
            GiamDoc = 6,
            [Description("Phó giám đốc")]
            PhoGiamDoc = 7,
            [Description("Trưởng phòng kỹ thuật")]
            TruongPhongKyThuat = 8,
            [Description("Phó phòng kỹ thuật")]
            PhoPhongKyThuat = 9,
            [Description("Đội trưởng")]
            DoiTruong = 10,
            [Description("Đội phó")]
            DoiPho = 11,
            [Description("Nhân viên")]
            NhanVien = 12,
            [Description("Công nhân")]
            CongNhan = 13,
            #endregion
        }
        // Trở ngại
        public enum DoiTuongCapNhatTroNgaiEnum
        {
            NhanVienKhaoSat = 1,
            TaiChinh,
            ThiCong
        }
        public enum NVKSCapNhatTroNgaiEnum
        {
            KhaoSatTaiCho = 1,
            TiepNhanLai,
            Huy,
            PhanCongLai
        }
        public enum TaiChinhCapNhatTroNgaiEnum
        {
            CoTheKhacPhuc = 2,
            KhongTheKhacPhuc
        }
        public enum ThiCongCapNhatTroNgaiEnum
        {
            ThiCongLai = 1,
            TiepNhanLai,
            Huy,
            PhanCongLai
        }

        // Hồ sơ
    }
}
