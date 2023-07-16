using System.ComponentModel;

namespace EVN.Core.Common
{
    public class AppEnum
    {
        public enum MenuStatusEnum
        {
            Active = 1,
            UnActive = 0
        }
        public enum PositionEnum
        {
            [Description("Quản trị viên")]
            Administrator = 0,

            [Description("Lãnh đạo B09")]
            LanhDaoB09 = 4,
            [Description("Chuyên viên B09")]
            ChuyenVienB09 = 3,
            [Description("Lãnh đạo B08")]
            LanhDaoB08 = 2,
            [Description("Chuyên viên B08")]
            ChuyenVienB08 = 1,

        }

        public enum DonGiaChietTinhPhanLoai
        {
            [Description("Nhân Công")]
            NhanCong = 1,
            [Description("Vật liệu")]
            VatLieu = 2,
            [Description("Máy thi công")]
            MayThiCong = 3,
        }

        public enum TinhTrangEnum
        {
            [Description("tạo mới")]
            TaoMoi = 0,
            [Description("gửi duyệt")]
            GuiDuyet = 1,
            [Description("duyệt")]
            DaDuyet = 2,
        }

        public enum PhanLoaiEnum
        {
            [Description("Vật liệu (ĐM 4970)")]
            PL1 = 1,
            [Description("VL-NC-MTC theo ĐM 4970")]
            PL2 = 2,
            [Description("VL_NC_MTC theo TT10/2019")]
            PL3 = 3,
            [Description("VL_NC_MTC theo 22/2020/QĐ-UBND")]
            PL4 = 4,
        }

        public enum TenCauHinhEnum
        {
            [Description("CP Chung")]
            CH1 = 1,
            [Description("CP nhà tạm")]
            CH2 = 2,
            [Description("CP công việc không XĐ được từ thiết kế")]
            CH3 = 3,
            [Description("Thu nhập chịu thuế TT")]
            CH4 = 4,

            [Description("CP chung (ĐM 4970)")]
            CH5 = 5,
            [Description("CP chung (TT12/2021)")]
            CH6 = 6,
            [Description("CP chung (ĐM 22/2020)")]
            CH7 = 7,
            [Description("CP công việc không XĐ được từ thiết kế")]
            CH8 = 8,
            [Description("Thu nhập chịu thuế TT")]
            CH9 = 9,
        }

        public enum PhanLoaiChietTinhEnum
        {
            [Description("Vật liệu")]
            VatLieu = 1,
            [Description("Nhân công")]
            NhanCong = 2,
            [Description("MTC")]
            MTC = 3,
        }
    }
}