using System.Collections.Generic;

namespace EVN.Core.Common
{
    public class AppConstants
    {
        public const string MainConnectionString = "MainDatabase";
        public const string TnkdMainConnectionString = "TnkdMainDatabase";
        //public const string DataTypes = "DataTypes";
        public const string SuperAdminRole = "Admin";
        public const string UserRole = "User";

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

        public class JwtClaimTypes
        {
            public const string UnitId = "UnitId";
        }

        public class Permissions
        {
            public const string ClaimType = "Permission";
            public const string AdminPermission = "admin";
            public const string All = "all";
            public const string View = ".view";
            public const string Create = ".create";
            public const string Update = ".update";
            public const string Delete = ".delete";
            public const string Approve = ".approve";
            public const string Receive = ".receive";
            public const string Assign = ".assign";
            public const string Role = "role";
            public const string RoleView = Role + View;
            public const string RoleCreate = Role + Create;
            public const string RoleUpdate = Role + Update;
            public const string RoleDelete = Role + Delete;
            public const string Department = "department";
            public const string DepartmentView = Department + View;
            public const string DepartmentCreate = Department + Create;
            public const string DepartmentUpdate = Department + Update;
            public const string DepartmentDelete = Department + Delete;
            public const string Profile = "profile";
            public const string approvepprove = ".approve";
            public const string Send = ".send";
            public const string Reject = ".reject";
            public const string Print = ".print";
            public const string ApplyPrice = ".apply_price";
            //public const string ProfileView = Profile + View;
            public const string ProfileCreate = Profile + Create;
            public const string ProfileUpdate = Profile + Update;
            public const string ProfileDelete = Profile + Delete;
            public const string Account = "account";
            public const string AccountView = Account + View;
            public const string AccountCreate = Account + Create;
            public const string AccountUpdate = Account + Update;
            public const string AccountDelete = Account + Delete;
            public const string Unit = "unit";
            public const string UnitView = Unit + View;
            public const string UnitCreate = Unit + Create;
            public const string UnitUpdate = Unit + Update;
            public const string UnitDelete = Unit + Delete;

            public const string FormReport = "formReport";
            public const string FormReportView = FormReport + View;
            public const string FormReportCreate = FormReport + Create;
            public const string FormReportUpdate = FormReport + Update;
            public const string FormReportSend = FormReport + Send;
            public const string FormReportApprove = FormReport + Approve;
            public const string FormReportReject = FormReport + Reject;


            public const string Agreement = "agreement";
            public const string AgreementView = Agreement + View;
            public const string AgreementCreate = Agreement + Create;
            public const string AgreementUpdate = Agreement + Update;
            public const string AgreementDelete = Agreement + Delete;

            public const string Construction = "construction";
            public const string ConstructionView = Construction + View;
            public const string ConstructionCreate = Construction + Create;
            public const string ConstructionSend = Construction + Send;
            public const string ConstructionUpdate = Construction + Update;
            public const string ConstructionDelete = Construction + Delete;
            public const string ConstructionApprove = Construction + Approve;
            public const string ConstructionReject = Construction + Reject;
            public const string ConstructionPrint = Construction + Print;

            public const string ConstructionApplyPrice = Construction + ApplyPrice;
            public const string ConstructionApplyPriceView = ConstructionApplyPrice + View;
            public const string ConstructionApplyPriceCreate = ConstructionApplyPrice + Create;
            public const string ConstructionApplyPriceUpdate = ConstructionApplyPrice + Update;
            public const string ConstructionApplyPriceDelete = ConstructionApplyPrice + Delete;

            public const string Substation = "substation";
            public const string SubstationView = Substation + View;
            public const string SubstationCreate = Substation + Create;
            public const string SubstationUpdate = Substation + Update;
            public const string SubstationDelete = Substation + Delete;

            public const string Equipment = "equipment";
            public const string EquipmentView = Equipment + View;
            public const string EquipmentCreate = Equipment + Create;
            public const string EquipmentUpdate = Equipment + Update;
            public const string EquipmentDelete = Equipment + Delete;

            public const string Request = "request";
            public const string RequestView = Request + View;
            public const string RequestCreate = Request + Create;
            public const string RequestUpdate = Request + Update;
            public const string RequestDelete = Request + Delete;
            public const string RequestApprove = Request + Approve;
            public const string RequestReceive = Request + Receive;
            public const string RequestSend = Request + Send;


            public const string Contact = "contact";
            public const string ContactView = Contact + View;
            public const string ContactCreate = Contact + Create;
            public const string ContactUpdate = Contact + Update;
            public const string ContactDelete = Contact + Delete;

            public const string Schedule = "schedule";
            public const string ScheduleView = Schedule + View;
            public const string ScheduleCreate = Schedule + Create;
            public const string ScheduleUpdate = Schedule + Update;
            public const string ScheduleDelete = Schedule + Delete;
            public const string ScheduleAssign = Schedule + Assign;

            public const string InternalAffair = "internalaffair";
            public const string InternalAffairView = InternalAffair + View;
            public const string InternalAffairCreate = InternalAffair + Create;
            public const string InternalAffairUpdate = InternalAffair + Update;
            public const string InternalAffairDelete = InternalAffair + Delete;
            public const string InternalAffairAssign = InternalAffair + Assign;

            public const string Immediary = "gridPower.immediary";
            public const string ImmediaryView = Immediary + View;
            public const string ImmediaryCreate = Immediary + Create;
            public const string ImmediaryUpdate = Immediary + Update;
            public const string ImmediaryDelete = Immediary + Delete;

            public const string Distribution = "gridPower.distribution";
            public const string DistributionView = Distribution + View;
            public const string DistributionCreate = Distribution + Create;
            public const string DistributionUpdate = Distribution + Update;
            public const string DistributionDelete = Distribution + Delete;

            public const string Line = "gridPower.line";
            public const string LineView = Line + View;
            public const string LineCreate = Line + Create;
            public const string LineUpdate = Line + Update;
            public const string LineDelete = Line + Delete;

            public const string Synchronize = "synchronize";
            public const string SynchronizeView = Synchronize + View;

            public const string PersonalWork = "personalWork";
            public const string PersonalWorkView = PersonalWork + View;
            public const string PersonalWorkCreate = PersonalWork + Create;
            public const string PersonalWorkUpdate = PersonalWork + Update;

            public const string EquipmentManagement = "equipmentManagement";
            public const string EquipmentManagementView = EquipmentManagement + View;
            public const string EquipmentManagementCreate = EquipmentManagement + Create;
            public const string EquipmentManagementUpdate = EquipmentManagement + Update;
            public const string EquipmentManagementDelete = EquipmentManagement + Delete;
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
            /// <summary>
            /// Công tơ 1 pha
            /// </summary>
            public static string CTO1 = "CTO";

            /// <summary>
            /// Công tơ 3 pha
            /// </summary>
            public static string CTO3 = "CTO";

            /// <summary>
            /// Máy biến điện áp 1 pha
            /// </summary>
            public static string TU1 = "TU";

            /// <summary>
            /// Máy biến điện áp 3 pha
            /// </summary>
            public static string TU3 = "TU";

            /// <summary>
            /// Máy biến dòng điện
            /// </summary>
            public static string TI = "TI";

            /// <summary>
            /// Ampemet
            /// </summary>
            public static string AMPEMET = "AM";

            /// <summary>
            /// Vonmet
            /// </summary>
            public static string VONMET = "VM";

            /// <summary>
            /// A.V có bộ chuyển đổi
            /// </summary>
            public static string AV = "AV";

            /// <summary>
            /// Teromet (10-3 - 103) ꭥ
            /// </summary>
            public static string TEROMET = "TE";

            /// <summary>
            /// Mêgômmét (104 - 109) ꭥ
            /// </summary>
            public static string MEGOMMET = "ME";

            public static List<string> AllCMISEquiqmentCode = new List<string> { CTO1, TU1, TI, AMPEMET, VONMET, AV, TEROMET, MEGOMMET };
        }
    }
}
