namespace EVN.Core.ConfigurationSettings
{
    public class AppSettings
    {
        public JwtConfig Jwt { get; set; }
        //public bool IsWriteLog { get; set; }
        public string[] AllowedHosts { get; set; }
        public GoogleAPISettings GoogleAPI { get; set; }
    }

    public class GoogleAPISettings
    {
        public string FilePathFCMConfig { get; set; }
        public string ServiceKeyAPI { get; set; }
        public string SenderId { get; set; }
        public string ApiMapKey { get; set; }
    }
}
