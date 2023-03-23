using System.IO;

namespace EVN.Core.Common
{
    public class RootPathConfig
    {
        private static readonly string Dirpath = Directory.GetCurrentDirectory();

        public class TemplatePath
        {
            public static readonly string GetTemplate = Dirpath +@"/App_Data/Template/";
        }

        public class TnkdTemplatePath
        {
            public static readonly string GetTemplate = Dirpath + @"\ExcelFiles\";
        }
    }
}
