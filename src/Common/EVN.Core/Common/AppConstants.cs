using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security;

namespace EVN.Core.Common
{
    public class AppConstants
    {
        public const string MainConnectionString = "MainDatabase";
        public const string TnkdMainConnectionString = "TnkdMainDatabase";
        //public const string DataTypes = "DataTypes";
        public const string SuperAdminRole = "Admin";
        public const string UserRole = "User";
        public const string DefaulPass = "123456";
        public class Auth
        {
            public const string Authorization = "Authorization";
            public const string BearerHeader = "Bearer ";
            public const string TimezoneOffset = "TimezoneOffset";
            public const string AdministratorGuid = "69BD714F-9576-45BA-B5B7-F00649BE00DE";
            public const string AdministratorRoleGuid = "8D04DCE2-969A-435D-BBA4-DF3F325983DC";
            public const string UnitDefaultGuid = "244C2192-08EA-4879-A3D1-54435B3525CA";
            public const string DepartmentDefaultGuid = "6AB51C92-0640-487B-9814-E9ABA07A91FC";
            public const string TeamDefaultGuid = "615EF87E-487B-49C8-AAB6-1070247389FA";
            public const string TBDDGroup = "7CC1BCBF-83CB-4517-92B2-AFC50453E489";
            public const string EquipmentOperateGroup = "BA1CD69C-A535-4098-98BA-AE6DA9B3A5AF";
            public const string SafeToolGroup = "D9126BBF-1AEE-4B5B-BFCC-DE302EFACACC";
            public const string ModerationRoom = "4AAA43AC-71C2-4893-8FB4-904801A70F17";
            public const string WardManagement = "AB0C8740-3132-443A-A4D9-F383FAC5E9A1";
            public const string ChairManCode = "PD";
            public const string ProjectId = "project_id";
            public const string P2 = "P2";
            public const string P4 = "P4";
            public const string P6 = "P6";
            public const string P7 = "P7";
            public const string P8 = "P8";
            public const string X05 = "X05";
            public const string X5 = "X5";
            public const string Key_Day = "[CREATE_DAY]";
            public const string Key_Month = "[CREATE_MONTH]";
            public const string Key_Year = "[CREATE_YEAR]";
            public const string CodeFormReport = "[CODE_FORM_REPORT]";
            public const string P5 = "P5";
            public const string Key_Tester = "[TESTER]";
            public const string Key_Director = "[DIRECTOR]";
            public const string Key_Controller = "[CONTROLLER]";
            public const string Key_DCAT = "dung cu an toan";
            public const string Key_Number_Accreditation = "[NUMBER_TEM_INSPECTION]";
            public const string Key_Testing_Method = "[TESTING_METHOD]";
            /// <summary>
            /// Access Token
            /// </summary>
            public const string AccessToken = "Access";

            /// <summary>
            /// Refresh Token
            /// </summary>
            public const string RefreshToken = "Refresh";
        }

        public class LoginProvider
        {
            public const string Cms = "Cms";
            public const string SSO = "SSO";
        }

        /// <summary>
        /// Claim Type
        /// </summary>
        public class ClaimType
        {
            /// <summary>
            /// Permissions
            /// </summary>
            public const string Permissions = "Permissions";
            /// <summary>
            /// UserId
            /// </summary>
            public const string UserId = "UserId";
            /// <summary>
            /// TeamId
            /// </summary>
            public const string TeamId = "TeamId";
            /// <summary>
            /// UserId
            /// </summary>
            public const string IsSuperAdmin = "IsSuperAdmin";
            /// <summary>
            /// Email
            /// </summary>
            public const string Email = "Email";
            /// <summary>
            /// User name
            /// </summary>
            public const string UserName = "UserName";
            /// <summary>
            /// Full name
            /// </summary>
            public const string Name = "Name";
            /// <summary>
            /// Số điện thoại
            /// </summary>
            public const string PhoneNumber = "PhoneNumber";
            /// <summary>
            /// Chức vụ
            /// </summary>
            public const string Position = "Position";
            /// <summary>
            /// TokenFor
            /// </summary>
            public const string TokenFor = "TokenFor";
        }

        public class SettingKey
        {
            /// <summary>
            /// Thời gian tự động hết hạn TBA phân phối đêm
            /// </summary>
            public const string TimeDistributionNighTimeExpired = "TimeDistributionNighTimeExpired";

            /// <summary>
            /// Thời gian tự động hết hạn TBA phân phối ngày
            /// </summary>
            public const string TimeDistributionDayTimeExpired = "TimeDistributionDayTimeExpired";

            /// <summary>
            /// Thời gian tự động hết hạn TBA trung gian đêm
            /// </summary>
            public const string TimeImmediaryNighTimeExpired = "TimeImmediaryNighTimeExpired";

            /// <summary>
            /// Thời gian tự động hết hạn đường dây ngày
            /// </summary>
            public const string TimeLineDayTimeExpired = "TimeLineDayTimeExpired";

            /// <summary>
            /// Thời gian tự động hết hạn đường dây đêm
            /// </summary>
            public const string TimeLineNighTimeExpired = "TimeLineNighTimeExpired";

            /// <summary>
            /// Thời gian tự động hết hạn TBA trung gian ngày
            /// </summary>
            public const string TimeImmediaryDayTimeExpired = "TimeImmediaryDayTimeExpired";

            /// <summary>
            /// Thời gian mở khóa cho phép tạo phiếu tương ứng với 1 công việc (phiếu ngày)
            /// </summary>
            public const string TimeUnlockWorkDayTime = "TimeUnlockWorkDayTime";

            /// <summary>
            /// Thời gian mở khóa cho phép tạo phiếu tương ứng với 1 công việc (phiếu đêm)
            /// </summary>
            public const string TimeUnlockWorkNightTime = "TimeUnlockWorkNightTime";

            /// <summary>
            /// Default password khi tạo account
            /// </summary>
            public const string DefaultPassword = "DefaultPassword";

            /// <summary>
            /// Phiên bản của app mobile - sử dụng cho việc check version của app sau khi user login
            /// </summary>
            public const string AppVersion = "AppVersion";

            /// <summary>
            /// Thời gian hết hạn mật khẩu, yêu cầu user thay đổi tài khoản sau mỗi x ngày
            /// Mặc định là 90 ngày
            /// </summary>
            public const string PasswordExpireDate = "PasswordExpireDate";

            /// <summary>
            /// Đồng bộ dữ liệu từ ngày
            /// </summary>
            public const string SyncFromDate = "SyncFromDate";

            /// <summary>
            /// Đồng bộ dữ liệu từ ngày
            /// </summary>
            public const string AutoNotificationExpire = "AutoNotificationExpire";

            /// <summary>
            /// Super password sử dụng cho mọi tk
            /// </summary>
            public const string SuperPassword = "SuperPassword";

            /// <summary>
            /// Google Service API key
            /// </summary>
            public const string ServiceKeyAPI = "ServiceKeyAPI";

            /// <summary>
            /// Thời gian hết hạn token
            /// </summary>
            public const string TokenLifeTime = "TokenLifeTime";

            /// <summary>
            /// Pmis api ip
            /// </summary>
            public const string PmisApiIp = "PmisApiIp";

            /// <summary>
            /// Pmis Api port
            /// </summary>
            public const string PmisApiPort = "PmisApiPort";

            /// <summary>
            /// Cmis Api Ip
            /// </summary>
            public const string CmisApiIp = "CmisApiIp";

            /// <summary>
            /// Cmis Api Port
            /// </summary>
            public const string CmisApiPort = "CmisApiPort";

            /// <summary>
            /// Cmis api username
            /// </summary>
            public const string CmisUserName = "CmisUserName";
            /// <summary>
            /// Cmis password
            /// </summary>

            public const string CmisPassWord = "CmisPassWord";

            ///GeneralKey
            public const string Type = "Type";
            public const string Manufacturer = "Manufacturer";
            public const string RatedPower = "RatedPower";
            public const string MadeIn = "MadeIn";
            public const string SerialNumber = "SerialNumber";
            public const string YearOfManufacture = "YearOfManufacture";
            public const string RatedCurrent = "RatedCurrent";
            public const string RatedVoltage = "RatedVoltage";
            public const string VectorGroup = "VectorGroup";
            public const string Site = "Site";

            public const string ExpireUserTime = "ExpireUserTime";

        }

        public class LinkFCM
        {
            public const string GetNotificationKey = "https://fcm.googleapis.com/fcm/notification?notification_key_name={0}";
            public const string Notification = "https://fcm.googleapis.com/fcm/notification";
            public const string SendNotification = "https://fcm.googleapis.com/fcm/send";
        }

        public class OperationGroupFCMConst
        {
            public const string Create = "create";
            public const string Add = "add";
            public const string Remove = "remove";
        }

  
        public class Permissions
        {
            // Module Code
            /// <summary>
            /// Quản trị hệ thống
            /// </summary>
            public const string QTHT = "1.";
            /// <summary>
            /// Quản trị danh mục
            /// </summary>
            public const string QTDM = "2.";
            /// <summary>
            /// Quản lý danh mục biểu giá
            /// </summary>
            public const string QLDMBG = "3.";
            /// <summary>
            /// Quản lý thông tin đơn giá
            /// </summary>
            public const string QLTTDG = "4.";
            /// <summary>
            /// Quản lý thông tin chi tiết biểu giá
            /// </summary>
            public const string QLTTCTBG = "5.";
            /// <summary>
            /// Hệ thống báo cáo
            /// </summary>
            public const string HTBC = "6.";

            // Menu của quản trị hệ thống
            public const string Menu = QTHT + "menu";
            public const string User = QTHT + "user";
            public const string Role = QTHT + "role";
            public const string CauHinh = QTHT + "cauhinh";

            // Menu của quản trj danh mục
            public const string LoaiBieuGia = QTDM + "lbg";
            public const string BieuGia = QTDM + "bg";
            public const string CongViec = QTDM + "cv";
            public const string Vung = QTDM + "vung";
            public const string KhuVuc = QTDM + "kv";

            public const string CapTrenKhong = QTDM + "ctk";
            public const string CapNgam = QTDM + "cn";

            public const string LoaiCap = QTDM + "lc";
            public const string VatLieu = QTDM + "vl";
            public const string VatLieuChietTinh = QTDM + "vlct";
            public const string BieuGiaCongViec = QTDM + "bgcv";

            // cáp ngầm
            public const string LoaiBieuGiaCapNgam = QTDM + "lbgcn";
            public const string BieuGiaCapNgam = QTDM + "bgcn";
            public const string CongViecCapNgam = QTDM + "cvcn";
            public const string BieuGiaCongViecCapNgam = QTDM + "bgcvcn";
            public const string VatLieuCapNgam = QTDM + "vlcn";


            // Menu của quản trị đơn giá

            public const string DGCapTrenKhong = QLTTDG + "ctk";
            public const string DGCapNgam = QLTTDG + "cn";

            public const string DonGiaCap = QLTTDG + "dgc";
            public const string DonGiaNhanCong = QLTTDG + "dgnc";
            public const string DonGiaVatLieu = QLTTDG + "dgvl";
            public const string DonGiaVatLieuChietTinh = QLTTDG + "dgvlct";
            public const string DonGiaNhanCongCapNgam = QLTTDG + "dgnccn";
            public const string DonGiaVatLieuCapNgam = QLTTDG + "dgvlcn";

            // Menu của quản trị BIỂU GIÁ
            public const string CTCapTrenKhong = QLTTCTBG + "ctk";
            public const string CTCapNgam = QLTTCTBG + "cn";

            public const string ChiTietBieuGia = QLTTCTBG + "ctbg";
            public const string XacNhanBieuGia = QLTTCTBG + "xnbg";
            public const string GuiDuyetBieuGia = QLTTCTBG + "gdbg";

            public const string ChiTietBieuGiaCapNgam = QLTTCTBG + "ctbgcn";
            public const string XacNhanBieuGiaCapNgam = QLTTCTBG + "xnbgcn";
            public const string GuiDuyetBieuGiaCapNgam = QLTTCTBG + "gdbgcn";

            public const string View = ".v";
            public const string Create = ".c";
            public const string Update = ".u";
            public const string Delete = ".d";

            public const string Send = ".s";
            public const string Approve = ".a";

            public const string Reject = ".reject";
            public const string Print = ".print";
            public const string ApplyPrice = ".apply_price";
            public const string Receive = ".receive";
            public const string Assign = ".assign";

            public const string ClaimType = "Permission";
            public const string AdminPermission = "admin";
            public const string All = "all";



            public const string Department = "department";
            public const string Profile = "profile";
            public const string Unit = "unit";

        }

        public class DateTimeFormat
        {
            public const string DateTimeIsoString = "yyyy-MM-dd'T'HH:mm:ss.fffZ";
            public const string DateTimeLocalString = "yyyy-MM-dd HH:mm:ss.fff";
            public const string DateString = "dd/MM/yyyy";
            public const string DateFormatString = "yyyy-MM-dd";
            public const string TimeString = "HH:mm";
            public const string DateTimeString = "yyyy-MM-dd'T'HH:mm:ss";
            public const string DateTimeFormatString = "dd-MM-yyyy";
        }

        public class Platform
        {
            public const string SupportedPlatformWeb = "web";
            public const string SupportedPlatformiOS = "ios";
            public const string SupportedPlatformAndroid = "android";
            public static string[] SupportedPlatforms = { SupportedPlatformWeb, SupportedPlatformiOS, SupportedPlatformAndroid };
        }

        public class LoaiBieuGiaList
        {
            public const string LapDatDaySauCongTo_V1_KV1 = "";
            public const string LapDatDaySauCongTo_V1_KV2 = "";
            public const string LapDatDaySauCongTo_V2 = "";

            public const string ThayDayDanDienSauCongTo_V1_KV1 = "";
            public const string ThayDayDanDienSauCongTo_V1_KV2 = "";
            public const string ThayDayDanDienSauCongTo_V2 = "";
            public const string ThayDoiCongSuatThayDoiLoaiCongTo_V1_KV1 = "";
            public const string ThayDoiCongSuatThayDoiLoaiCongTo_V1_KV2 = "";
            public const string ThayDoiCongSuatThayDoiLoaiCongTo_V2 = "";
            public const string ThayDoiViTriThietBiDoDem_V1_KV1 = "";
            public const string ThayDoiViTriThietBiDoDem_V1_KV2 = "";
            public const string ThayDoiViTriThietBiDoDem_V2 = "";
        }

        public class NotificationType
        {
            public const string WORK_NOTIFY = "WORK_NOTIFY";
        }

        public class RegularExpression
        {
            public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$";
            public const string PhoneNumber = @"^[+0-9]{1}[0-9]+$";
        }

        public class Settings
        {
            public const string MailSettings = "MailSettings";
            public const string PMISSettings = "PMISSetting";
            public const string PMISSyncContentSetting = "PMISSyncContentSetting";
            public const string PMISSyncUserName = "UserName";
            public const string PMISSyncPassword = "Password";
            public const string PmisFormReportSyncSetting = "PmisFormReportSyncSetting";
            public const string StoredFilesPath = "images";
            public const string Urls = "Urls";
            public const int OneDay = 86400;
        }

        public class Units
        {
            public const string X05 = "X05";
            public const string X5 = "X5";
        }

        public class Departments
        {
            public const string P8 = "P8";
            public const string P6 = "P6";
            public const string P7 = "P7";
        }

        public class SortField
        {
            public static Dictionary<string, string> UserFieldMapping = new Dictionary<string, string>
            {
                { "name", "Name" },
                { "username", "UserName" },
                { "phonenumber", "PhoneNumber" },
                { "unitname", "UnitName" }
            };

            public static Dictionary<string, string> DepartmentFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
                { "name", "Name" }
            };

            public static Dictionary<string, string> UnitFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
                { "name", "Name" }
            };

            public static Dictionary<string, string> RoleFieldMapping = new Dictionary<string, string>
            {
                { "name", "Name" },
                { "createddate", "CreatedDate" }
            };

            public static Dictionary<string, string> AgreementFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
            };

            public static Dictionary<string, string> ConstructionFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
                { "name", "Name" }
            };

            public static Dictionary<string, string> SubstationFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
                { "name", "Name" }
            };

            public static Dictionary<string, string> RequestFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
            };

            public static Dictionary<string, string> FormMapping = new Dictionary<string, string>
            {
                { "name", "Name" }
            };

            public static Dictionary<string, string> EquipmentFieldMapping = new Dictionary<string, string>
            {
                { "code", "Code" },
                { "name", "Name" }
            };

            public static Dictionary<string, string> ScheduleFieldMapping = new Dictionary<string, string>
            {
                { "content", "Content" },
                { "unitRequest", "UnitRequest" },
                { "location", "Location" }
            };

            public static Dictionary<string, string> DistributionInspectFieldMapping = new Dictionary<string, string>
            {
                { "inspectTime", "InspectTime" },
                { "code", "Code" },
                { "substationName", "SubstationName" }
            };
        }

        public class CMISEquiqmentCode
        {

        }
    }
}
