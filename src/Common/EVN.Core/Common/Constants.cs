namespace EVN.Core.Common
{
    public static class Constants
    {
        public static class NumberFormat
        {
            public const string Percentage = "0.0%";
            public const string Number = "0.0";
            public const string Decimal = "#,##0.000";
            public const string Decimal1 = "#,##0.###";
            public const string Decimal2 = "#,##0.00";
            public const string Number1 = "#,##0";
            public const string Number2 = "0";
        }

        public class AssetManagementUnitConst
        {
            public const string Customer = "KH";
            public const string Electricity = "EVN HANOI";
        }

        /// <summary>
        /// Datetime format
        /// </summary>
        public static class DateTimeFormat
        {
            public const string FullDateTime = "dd/MM/yyyy HH:mm:ss";
            public const string FullHour = "HH:mm:ss";
            public const string DdMmyyyy = "dd/MM/yyyy";
            public const string Hour = "HH:mm";
            public const string DateAndHour = "dd/MM/yyyy HH:mm";
            public const string SimpleDate = "yyMMdd";
            public const string DateOnly = "dd";
            public const string IsoString = "yyyy-MM-ddTHH:mm:ssZ";
            public const string IsoStringWithoutZ = "yyyy-MM-ddTHH:mm:ss";
            public const string CommonDate = "yyyyMMddHHmmss";
            public const string YyyyMmDd = "yyyy/MM/dd";
        }

        public const int DEFAULT_SPLIT_SIZE = 10;

        public class InspectionCategoryType
        {
            // Các loại thiết bị trên PMIS
            // Lấy theo API trên PMIS http://42.112.213.225:8051/Service.asmx/PM_DM_ALL?id=13&iddv=&id_loai_congviec=&Thang=&Nam=
            public const string COMPACT = "Bộ Compact";
            public const string APTOMAT = "Aptomat";
            public const string BATTERY = "Ắc quy";
            public const string UNDERGROUND_CABLES = "Dây cáp ngầm";
            public const string LINE_POLE = "Cột điện";
            public const string LOAD_BREAKER = "Cầu dao phụ tải";
            public const string POWER_CABLE = "Cáp lực";
            public const string LIGHTNING_CONDUCTOR = "Chống sét";
            public const string LIGHTNING_PROTECTION_NEEDLE = "Chống sét kim";
            public const string LINE_LIGHTNING_ARRESTER = "Chống sét van";
            public const string BREAKER = "Cầu dao";
            public const string LIGHTNING_RODS = "Dây chống sét";
            public const string LINE_WIRE = "Dây dẫn";
            public const string AUTOMATIC_EQUIPMENT = "Thiết bị tự động và đo lường";
            public const string KNIVES_GROUND = "Dao tiếp địa";
            public const string FALLOFFUSE = "Cầu chì tự rơi";
            public const string POLE_CLAMP = "Kẹp cực";
            public const string ELECTRICAL_RESISTANCE = "Kháng điện";
            public const string TRANSFORMER = "Máy biến áp";
            public const string COILS_TRANSFORMERS = "Cuộn - Máy biến áp";
            public const string FAN_TRANSFORMERS = "Quạt - Máy biến áp";
            public const string SELF_USE_TRANSFORMERS = "Máy biến áp tự dùng";
            public const string CUTTING_MACHINE = "Máy cắt";
            public const string LINE_FUDAMENT = "Móng cột";
            public const string LINE_PINCHED = "Móng néo";
            public const string MEDIUM_VOLTAGE_CABINET = "Ngăn tủ trung thế";
            public const string PRESSURE_REGULATOR = "Bộ điều áp dưới tải";
            public const string OLTC = "Nấc OLTC";
            public const string RECLOSER = "Recloser";
            public const string ROLE = "Rơ le";
            public const string CERAMIC = "Sứ";
            public const string CAPACITOR = "Tụ bù";
            public const string MEASURING_EQUIPMENT = "Thiết bị đo đếm";
            public const string MEASURING_DEVICE = "Thiết bị đo lường";
            public const string BUSBAR = "Thanh cái";
            public const string ANTI_SHAKE_WEIGHT = "Tạ chống rung";
            public const string TI = "Biến dòng điện";
            public const string BATTERY_CHARGING_CABINET = "Tủ nạp ắc quy";
            public const string TU = "Biến điện áp";
            public const string UNDERGROUND_CABLE_CABINETS = "Tủ cáp ngầm";
            public const string LOW_PRESSURE_CABINET = "Tủ hạ thế";
            public const string RMU = "Tủ RMU";
            public const string LINE_BEAM = "Xà";

            // Các loại vật tư này không được liệt kê trong danh sách Loại thiết bị trên PMIS => comment
            // // Trạm phân phối
            // public const string INSULATION = "Cách điện";
            // public const string GROUNDING_SYSTEM = "Hệ thống nối đất";
            // public const string CONSTRUCTION_STRUCTURE = "Kết cấu xây dựng";
            //
            // // Trạm trung gian
            // public const string DISCONNECTORS_SWITCHES = "Dao cách ly";
            // public const string CUTTER_LBS = "Dao cắt có tải LBS";
            // public const string VARIABLE_VOLTAGE = "Biến điện áp";
            // public const string CURRENT_TRANSFORMER = "Biến dòng điện";
            // public const string CABLE_HEAD = "Đầu cáp";
            // public const string HIGH_PRESSURE_CABLE = "Cáp lực cao áp";
            // public const string LOW_PRESSURE_CABLE = "Cáp lực hạ áp";
            // public const string JOINT = "Mối nối";
            // public const string ONE_WAY_SYSTEM = "Hệ thống 1 chiều";
            // public const string ALTERNATING_CURRENT_SYSTEM = "Hệ thống xoay chiều";
            // public const string FILLING_CABINET = "Tủ nạp";
            // public const string MEASURING_SYSTEM = "Hệ thống đo";
            // public const string ELECTRIC_CABINET = "Tủ điện";
            // public const string CLAMP_ROW = "Hàng kẹp và các đầu nối nhị thứ";
            // public const string RESISTANCE_TEMPERATURE_DETECTOR = "Hệ thống RTĐ, tự dùng, chiếu sáng";
            // public const string IMMEDIARY_STATION_CLEANING = "Tình hình vệ sinh trạm";

            //Đường dây
            public const string Node = "Nút";
            public const string Line = "Đường dây";
            public const string Substation = "Trạm biến áp";
            // public const string LINE_FALLOFFUSE = "Cầu chì tự rơi";
        }

        public class PmisSubstationType
        {
            public const string Distribution = "Trạm phân phối";
            public const string Immediary = "Trạm trung gian";
            public const string Cutting = "Trạm cắt";
        }

        public class WorkType
        {
            public const string DistributionSubstation = "OP_TT_TBA_NGAY";
            public const string DistributionSubstationNightTime = "OP_TT_TBA_DEM";
            public const string Line = "OP_TT_DZ_NGAY";
            public const string LineNightTime = "OP_TT_DZ_DEM";
            public const string Position = "OP_VITRI_KT";
            public const string ImmediarySubstationDayTime = "OP_TT_NGAY_TG";
            public const string ImmediarySubstation = "OP_TT_TRAM_TG";
            public const string ImmediarySubstationNightTime = "OP_TT_DEM_TG";
            public const string InspectList = "OP_DANHSACH_KT";
        }

        public class PmisMessage
        {
            public const string NONE = "Không có dữ liệu";
        }

        public class ApplicationSettingKey
        {
            public const string ScheduleAutoSyncHour = "SyncJob:Hours";
            public const string ScheduleAutoSyncMinute = "SyncJob:Minutes";
        }

        public class FrequencyType
        {
            public const string DAY = "DAY";
            public const string MONTH = "MONTH";
            public const string YEAR = "YEAR";
        }

        public class TUTIConstant
        {
            public const string TEROMET = "Teromet";
            public const string MEGOMET = "Megomet";
            public const string MEGO_EQUIPMENT_TYPE = "Mêgômmét (104 - 109) ꭥ";
            public const string TERO_EQUIPMENT_TYPE = "Teromet (10-3 - 103) ꭥ";
            public const string MEGO_EQUIPMENT_TYPE_DB = "Meegommet (104-109)";
            public const string TERO_EQUIPMENT_TYPE_DB = "Teromet (10-3-103)";
            public const string METER = "Công tơ";
            public const string METER_1 = "Công tơ 1 pha";
            public const string METER_3 = "Công tơ 3 pha";
            public const string TU = "TU";
            public const string TI = "TI";
            public const string TU_NAME = "Máy biến điện áp";
            public const string TI_NAME = "Máy biến dòng điện";
            public const string METER_CMIS = "CTO";
            public const string TI_HA_THE = "Máy biến dòng điện đo lường hạ thế";
            public const string TI_TRUNG_THE = "Máy biến dòng điện đo lường trung thế";
            public const string TU_TRUNG_THE = "Máy biến điện áp đo lường trung thế";
        }

        public struct CONFIG_KEYS
        {
            public const string JWT_TOKEN = "JwtTokenSettings";
            public const string APP_CONNECTION_STRING = "MainDatabase";
            public const string SMTP_SETTING = "SmtpSetting";
            public const string MEDIA_SETTING = "MediaSetting";
            public const string GOOGLE_SETTING = "GoogleSetting";
            public const string FACEBOOK_SETTING = "FacebookSetting";
            public const string FIREBASE_SETTING = "FirebaseSetting";
            public const string AGGREGATOR_BFF_SETTING = "AggregatorBff";
            public const string MAIL_SETTING = "MailSetting";
            public const string APPLICATION_URL = "ApplicationUrl";
        }
        public struct VERTICAL_ALIGNMENTS
        {
            public const string TOP = "Top";
            public const string CENTER = "Center";
            public const string BOTTOM = "Bottom";
            public const string DISTRIBUTED = "Distributed";
            public const string JUSTIFY = "Justify";
        }

        public struct HORIZONT_ALALIGNMENTS
        {
            public const string GENERAL = "General";
            public const string LEFT = "Left";
            public const string CENTER = "Center";
            public const string CENTER_CONTINUOUS = "CenterContinuous";
            public const string RIGHT = "Right";
            public const string FILL = "Fill";
            public const string DISTRIBUTED = "Distributed";
            public const string JUSTIFY = "Justify";
        }
    }
}
