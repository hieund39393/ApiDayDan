using System.IO;

namespace EVN.Core.Common
{
    public class RootPathConfig
    {
        private static readonly string Dirpath = Directory.GetCurrentDirectory();

        public class TemplatePath
        {
            public static readonly string GetTemplate = Dirpath +@"/Templates/";
        }

        public class TnkdTemplatePath
        {
            public static readonly string GetTemplate = Dirpath + @"\ExcelFiles\";
        }
    }
}
