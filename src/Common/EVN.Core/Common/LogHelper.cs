using Serilog;

namespace EVN.Core.Common
{
    public class LogHelper
    {
        public static ILogger Logger;
        public static ILogger ErrorSystemLogger;
        public static ILogger InternalSystemLogger;
    }
}
