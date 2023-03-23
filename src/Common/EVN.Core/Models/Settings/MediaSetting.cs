using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVN.Core.Models.Settings
{
    public class MediaSetting
    {
        public string ServerName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public MediaFolders Folders { get; set; }
        public long MaxFileSize { get; set; }
    }

    public class MediaFolders
    {
        public string Root { get; set; }
        public string Files { get; set; }
        public string Images { get; set; }
        public string Videos { get; set; }
    }
}
