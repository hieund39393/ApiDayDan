﻿using EVN.Core.Attributes;
using EVN.Core.Properties;
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
    }
}