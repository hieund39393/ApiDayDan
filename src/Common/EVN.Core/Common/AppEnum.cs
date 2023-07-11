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
            [Description("Phòng vật tư")]
            PhongVatTu = 14,
            [Description("Nhân viên treo tháo")]
            NVTT = 15,
            [Description("Nhân viên niêm phong")]
            NVNP = 16,
            #endregion
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
        }
    }
}