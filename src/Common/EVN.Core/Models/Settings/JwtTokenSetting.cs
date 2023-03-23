using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVN.Core.Models.Settings
{
    public class JwtTokenSetting
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; }
        public int PasswordMinutes { get; set; }
        public int TokenExtension { get; set; }
    }
}
